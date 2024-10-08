using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sound manager to handle various sounds in the different scenes
/// </summary>
public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager instance
    {
        get => _instance;
        private set
        {
            if (_instance != null)
                Debug.LogWarning("Second attempt to get SoundManager");
            _instance = value;
        }
    }

    [Header("Audiclips for interactions")]
    [SerializeField]
    private AudioClip audiDuck;
    [SerializeField] 
    private AudioClip audiDuckEnd;
    [SerializeField]
    private AudioClip audiStart;
    [SerializeField]
    private AudioClip audiError;
    [SerializeField]
    private AudioClip audiAlarm;
    [SerializeField]
    private AudioClip audiDrill;
    [SerializeField]
    private AudioClip audiSpray;

    [SerializeField]
    private AudioSource audiSource;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!audiSource)
            audiSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDuck()
    {
        if(audiDuck)
        {
            audiSource.PlayOneShot(audiDuck);
        }
    }

    public void PlayDuckEnd()
    {
        if (audiDuckEnd)
        {
            audiSource.PlayOneShot(audiDuckEnd);
        }
    }

    public void PlayDuckStart() 
    {
        if (audiStart)
        {
            audiSource.PlayOneShot(audiStart);
        }
    }

    public void PlayError()
    {
        if (audiError)
        {
            audiSource.PlayOneShot(audiError);
        }
    }

    public void PlayAlarm()
    {
        if(audiAlarm)
        {
            audiSource.PlayOneShot(audiAlarm);
        }
    }

    public void PlayDrill()
    {
        if(audiDrill)
        {
            audiSource.PlayOneShot(audiDrill);
        }
    }

    public void PlaySpray()
    {
        if(audiSpray)
        {
            audiSource.PlayOneShot(audiSpray);
        }
    }

    public void StopSound()
    {
        audiSource.Stop();
    }
}
