using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

/// <summary>
/// Manager in charge of the haptic effect in the Haptics Scene covering buffered and unbuffered
/// </summary>
public class HapticsDemoManager : MonoBehaviour
{
    private static HapticsDemoManager _instance;
    public static HapticsDemoManager instance
    {
        get => _instance;
        private set
        {
            if (_instance != null)
                Debug.LogWarning("Second attempt to get HapticsDemoManager");
            _instance = value;
        }
    }

    [Header("Player Components")]
    [SerializeField]
    [Tooltip("Container for right controller visual")]
    private GameObject rightControllerVisual;
    [SerializeField]
    [Tooltip("Container for left controller visuals")]
    private GameObject leftControllerVisual;
    [SerializeField]
    [Tooltip("XRI Hapic controller on the left controller")]
    private HapticImpulsePlayer leftHapticPlayer;
    

    [Header("Drill Components")]
    [SerializeField]
    [Tooltip("Audio Clip for buffered haptics on the drill")]
    private AudioClip drillClip;
    //int cache for the buffered drillclip haptics
    private int drillLeftCache;
    private int drillRightCache;
    //bool to control if the controller can handle buffered haptics
    private bool canUseBufferedHaptics = false;

    [Header("Spray Components")]
    [SerializeField]
    [Tooltip("Particle effect on the spray can")]
    private ParticleSystem sprayParticleSystem;

    [Header("Alarm Components")]
    [SerializeField]
    [Tooltip("Controls the bell alarm movement")]
    private Animator alarmAnim;
    //bool to control alarm
    private bool alarmOn = false;

    [Header("Duck Components")]
    [SerializeField]
    [Tooltip("Controls the duck reaction")]
    private Animator duckAnim;


    private void Awake()
    {
        instance = this;
#if !UNITY_EDITOR
        CheckForBufferedHaptics();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        if (canUseBufferedHaptics)
        {
            CacheBufferedHaptics();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Check if the current device can use BufferedHaptics
    /// </summary>
    private void CheckForBufferedHaptics()
    {
        if (PXR_Input.GetControllerDeviceType() == PXR_Input.ControllerDevice.PICO_4 ||
            PXR_Input.GetControllerDeviceType() == PXR_Input.ControllerDevice.PICO_4U)
        {
            canUseBufferedHaptics = true;
            Debug.Log("Buffered Haptics avaialble");
        }
        else
            Debug.Log("Buffered Haptics not available on Device");
    }

    /// <summary>
    /// Controls the visual state of the Right Controller
    /// </summary>
    /// <param name="enbaledState">Active visual state of controller</param>
    public void RightControllerVisualState(bool enbaledState)
    {
        rightControllerVisual.SetActive(enbaledState);
    }
    /// <summary>
    /// Controls the visual state of the Left Controller
    /// </summary>
    /// <param name="enbaledState">Active visual state of controller</param>
    public void LeftControllerVisualState(bool enbaledState)
    {
        leftControllerVisual.SetActive(enbaledState);
    }

    /// <summary>
    /// Enable the Haptics on both controllers.
    /// The right controller is the PICO SDK method
    /// The left controller is the Unity XRI method
    /// </summary>
    /// <param name="amplitude">0-1f vibration strength</param>
    /// <param name="duration">PICO is in int and milliseconds. Unity is float and seconds</param>
    private void TriggerBothControllers(float amplitude, int duration)
    {
        //PICO is in int and milliseconds. Default frequency is 150
        PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.RightController, amplitude, duration);
        //Unity is float and seconds. Frequency of 0 will use the default frequency of device which is 150 for PICO
        float unityTime = duration / 1000f;
        leftHapticPlayer.SendHapticImpulse(amplitude, duration, 0);
    }

    /// <summary>
    /// Haptics to be triggered on both controllers when play hovers with near or far interactors
    /// </summary>
    public void OnDuckHover()
    {
        SoundManager.instance.PlayDuck();//Play duck sound
        duckAnim.SetTrigger("Duck");//Trigger the duck squish animation
        TriggerBothControllers(1f, 250);//Trigger both controller haptics
    }

    /// <summary>
    /// Haptics enabled when the player pokes the button on the alarm and alarmOn becomes true.
    /// Poke again to turn off the alarm
    /// </summary>
    public void AlarmToggle()
    {
        alarmOn = !alarmOn;

        if (alarmOn)
        {
            alarmAnim.SetTrigger("AlarmOn");//Trigger the alarm on animation
            InvokeRepeating("AlarmEnabled", 0, .5f);//Allow the repeating of the alarm and haptics for indefinite amount
        }
        else
        {
            alarmAnim.SetTrigger("AlarmOff");//Trigger the alarm off anim
            CancelInvoke("AlarmEnabled");
            SoundManager.instance.StopSound();
        }
    }

    /// <summary>
    /// When alarmOn is true, haptics enabled on both controllers
    /// </summary>
    private void AlarmEnabled()
    {
        SoundManager.instance.PlayAlarm();
        TriggerBothControllers(1f, 250);
    }


    /// <summary>
    /// Cache an audioclip to be used as a buffered haptic for each controller. Only for PICO4 and later. 
    /// Length of haptic will be the same as the audio clip used.
    /// </summary>
    private void CacheBufferedHaptics()
    {
        PXR_Input.SendHapticBuffer(PXR_Input.VibrateType.RightController, drillClip, PXR_Input.ChannelFlip.No, ref drillRightCache, PXR_Input.CacheType.CacheNoVibrate);
        PXR_Input.SendHapticBuffer(PXR_Input.VibrateType.LeftController, drillClip, PXR_Input.ChannelFlip.No, ref drillLeftCache, PXR_Input.CacheType.CacheNoVibrate);
    }

    /// <summary>
    /// Start Buffered Haptics on right controller. Length of haptic will be the same as the audio clip used.
    /// </summary>
    /// <param name="enabledState">Controller by HapticSelect script on the haptic gameobjects</param>
    public void StartRightDrill(bool enabledState)
    {

        if (enabledState)
        {
            PXR_Input.StartHapticBuffer(drillRightCache);
            SoundManager.instance.PlayDrill();
        }
        else
        {
            PXR_Input.StopHapticBuffer(drillRightCache);
            SoundManager.instance.StopSound();
        }
    }

    /// <summary>
    /// Start Buffered Haptics on left controller. Length of haptic will be the same as the audio clip used.
    /// </summary>
    /// <param name="enabledState">Controller by HapticSelect script on the haptic gameobjects</param>
    public void StartLeftDrill(bool enabledState)
    {
        if (enabledState)
        {
            PXR_Input.StartHapticBuffer(drillLeftCache);
            SoundManager.instance.PlayDrill();
        }
        else
        {
            PXR_Input.StopHapticBuffer(drillLeftCache);
            SoundManager.instance.StopSound();
        }
        
    }

    /// <summary>
    /// Short burst of the particle effect on the spray can with a right unbuffered haptic effect
    /// PICO's Haptic impulse is in milliseconds and int datatype
    /// </summary>
    /// <param name="enabledState">Controller by HapticSelect script on the haptic gameobjects</param>
    public void StartRightSpray(bool enabledState)
    {
        if(enabledState) 
        {
            PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.RightController, .8f, 250);
            SoundManager.instance.PlaySpray();
            sprayParticleSystem.Play();
        }
        else
        {
            PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.RightController, 0, 0);
            SoundManager.instance.StopSound();
        }
    }

    /// <summary>
    /// Short burse of the particle effect on the spray can with a left unbuffered haptic effect
    /// Unity's Haptic impulse is in seconds and in float datatype
    /// </summary>
    /// <param name="enabledState">Controller by HapticSelect script on the haptic gameobjects</param>
    public void StartLeftSpray(bool enabledState)
    {
        if(enabledState) 
        {
            leftHapticPlayer.SendHapticImpulse(.8f, .25f, 0);
            SoundManager.instance.PlaySpray();
            sprayParticleSystem.Play();
        }
        else
        {
            leftHapticPlayer.SendHapticImpulse(0, 0, 0);
            SoundManager.instance.StopSound();
        }
    }

}
