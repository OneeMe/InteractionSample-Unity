using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

/// <summary>
/// Used with XR Simple Interactable's Interactable Events
/// Script located on objects that we want a haptic interaction by using enum HapticDemoType
/// to select the object type which will determine the haptic effect to fire at specific
/// Interactable events on the Simple Interactable or Grab Interactable
/// Interactable events triggered by Interactor script on the controller will have a handiness to determine if it is the left or right
/// </summary>
public class HapticsSelector : MonoBehaviour
{
    //Enum of the various haptic object types
    public enum HapticDemoType
    {
        Default = 0,
        Duck,
        Drill,
        Alarm,
        Spray,
    }
    [Tooltip("Select Haptic Object type")]
    public HapticDemoType hapticDemoType = HapticDemoType.Default;

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// Interactable events on the Simple Interactable or Grab Interactable
    /// On Hover events
    /// </summary>
    /// <param name="args"></param>
    public void OnHover(HoverEnterEventArgs args)
    {
        if (args == null)
        {
            Debug.LogWarning("Event Args not available");
            return;
        }

        switch (hapticDemoType)
        {
            case HapticDemoType.Duck:
                HapticsDemoManager.instance.OnDuckHover();
                break;
        }

    }

    /// <summary>
    /// Interactable events on the Simple Interactable or Grab Interactable
    /// </summary>
    /// <param name="args"></param>
    public void OnSelectedEntered(SelectEnterEventArgs args)
    {
        if (args == null)
        {
            Debug.LogWarning("Event Args not available");
            return;
        }

        if (args.interactorObject != null)
        {
            //Determine the left or right hand of the Interactor
            var currenthand = args.interactorObject.handedness;

            if (currenthand == InteractorHandedness.Right)
            {
                switch (hapticDemoType)
                {
                    //Hide the controller if you grab the drill or spray can
                    case HapticDemoType.Drill:
                    case HapticDemoType.Spray:
                        HapticsDemoManager.instance.RightControllerVisualState(false);
                        break;
                    //Turn the Alarm on or off
                    case HapticDemoType.Alarm:
                        HapticsDemoManager.instance.AlarmToggle();
                        break;
                }
            }
            else
            {
                switch (hapticDemoType)
                {
                    //Hide the controller if you grab the drill or spray can
                    case HapticDemoType.Drill:
                    case HapticDemoType.Spray:
                        HapticsDemoManager.instance.LeftControllerVisualState(false);
                        break;
                    //Turn the Alarm on or off
                    case HapticDemoType.Alarm:
                        HapticsDemoManager.instance.AlarmToggle();
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Interactable events on the Simple Interactable or Grab Interactable
    /// </summary>
    /// <param name="args"></param>
    public void OnSelectExit(SelectExitEventArgs args)
    {
        if (args == null)
        {
            Debug.LogWarning("Event Args not available");
            return;
        }

        if (args.interactorObject != null)
        {
            var currenthand = args.interactorObject.handedness;

            if (currenthand == InteractorHandedness.Right)
            {
                switch (hapticDemoType)
                {
                    //Turn the controller visual back on
                    case HapticDemoType.Drill:
                    case HapticDemoType.Spray:
                        HapticsDemoManager.instance.RightControllerVisualState(true);
                        break;
                }
            }
            else
            {
                switch (hapticDemoType)
                {
                    //Turn the controller visual back on
                    case HapticDemoType.Drill:
                    case HapticDemoType.Spray:
                        HapticsDemoManager.instance.LeftControllerVisualState(true);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Interactable events on the Simple Interactable or Grab Interactable
    /// </summary>
    /// <param name="args"></param>
    public void OnActivate(ActivateEventArgs args)
    {
        if (args == null)
        {
            Debug.LogWarning("Event Args not available");
            return;
        }

        if (args.interactorObject != null)
        {
            var currenthand = args.interactorObject.handedness;

            if(currenthand == InteractorHandedness.Right)
            {
                switch(hapticDemoType) 
                {
                    //Activate the action effect
                    case HapticDemoType.Drill:
                        HapticsDemoManager.instance.StartRightDrill(true);
                        break;
                    case HapticDemoType.Spray:
                        HapticsDemoManager.instance.StartRightSpray(true);
                        break;
                }
            }
            else
            {
                switch (hapticDemoType)
                {
                    //Activate the action effect
                    case HapticDemoType.Drill:
                        HapticsDemoManager.instance.StartLeftDrill(true);
                        break;
                    case HapticDemoType.Spray:
                        HapticsDemoManager.instance.StartLeftSpray(true);
                        break;

                }
            }
        }
    }

    /// <summary>
    /// Interactable events on the Simple Interactable or Grab Interactable
    /// </summary>
    /// <param name="args"></param>
    public void OnDeactivate(DeactivateEventArgs args)
    {
        if (args == null)
        {
            Debug.LogWarning("Event Args not available");
            return;
        }

        if (args.interactorObject != null)
        {
            var currenthand = args.interactorObject.handedness;

            if (currenthand == InteractorHandedness.Right)
            {
                switch (hapticDemoType)
                {
                    //Turn the effects off
                    case HapticDemoType.Drill:
                        HapticsDemoManager.instance.StartRightDrill(false);
                        break;
                    case HapticDemoType.Spray:
                        HapticsDemoManager.instance.StartLeftSpray(false);
                        break;
                }
            }
            else
            {
                switch (hapticDemoType)
                {
                    //Turn the effects off
                    case HapticDemoType.Drill:
                        HapticsDemoManager.instance.StartLeftDrill(false);
                        break;
                    case HapticDemoType.Spray:
                        HapticsDemoManager.instance.StartLeftSpray(false);
                        break;
                }
            }
        }
    }
}
