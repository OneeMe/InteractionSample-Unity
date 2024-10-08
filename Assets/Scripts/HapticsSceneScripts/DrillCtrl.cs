using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script on the drill to add a visual shaking effect when the trigger is being held
/// and the Drill is in it's active state
/// </summary>
public class DrillCtrl : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Container for drill visual")]
    private Transform drillVisual;
    [SerializeField]
    [Tooltip("Adjust the intensity of the visual shaking effect")]
    private float vibrationIntensity = 0.03f;
    [SerializeField]
    [Tooltip("Adjust the speed of the visual shaking effect")]
    private float vibrationSpeed = 0.03f;
    [SerializeField]
    [Tooltip("Audioclip of the drill sound to get the max time of effect")]
    private AudioClip drillClip;

    private Vector3 startPos;//Cache start position for return position
    private Quaternion startRot;//Cache the starting rotation for a return rotation
    private bool isDrillOn = false;//Logic control of drll
    private float maxTime = 0;//Max time will be obtained from drillClip
    private float currTime = 0;//how long the drill has been active
    
    // Start is called before the first frame update
    void Start()
    {
        //Cache our starting position and rotation
        startPos = drillVisual.localPosition;
        startRot = drillVisual.localRotation;
        //Get our max time
        if (drillClip)
            maxTime = drillClip.length;
    }

    // Update is called once per frame
    void Update()
    {
        //logic to control how long the drill should run and what happens when it ends
        if(isDrillOn)
        {
            currTime += Time.deltaTime;
            if(currTime < maxTime)
            {
                DrillVibration();
            }
            else
            {
                OnDrillDeActivate();
            }
        }
    }

    /// <summary>
    /// When player triggers the drill, enable the drill logic bool isDrillOn
    /// </summary>
    public void OnDrillActivate()
    {
        currTime = 0;
        isDrillOn = true;
    }

    /// <summary>
    /// While isDrillOn is true, we will find a random position and rotation and Lerp the drill to the new position constantly
    /// This will be our visual shaking effect.
    /// </summary>
    private void DrillVibration()
    {
        //Find random positions and rotations based on our start position and rotation
        Vector3 randomPosition = startPos + Random.insideUnitSphere * vibrationIntensity;
        Quaternion randomRotation = startRot * Quaternion.Euler(Random.insideUnitSphere * vibrationIntensity * 10f);

        // Apply the random offset to the hand drill in its Local area so it is still in the players hand
        drillVisual.localPosition = Vector3.Lerp(drillVisual.localPosition, randomPosition, vibrationSpeed);
        drillVisual.localRotation = Quaternion.Slerp(drillVisual.localRotation, randomRotation, vibrationSpeed);
    }

    /// <summary>
    /// When the drill is off, we move the drill back in its original local position in the players hand
    /// </summary>
    public void OnDrillDeActivate()
    {
        isDrillOn = false;
        drillVisual.localPosition = startPos;
        drillVisual.localRotation = startRot;
    }
}
