using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manager to handle hand tracking interaction scene
/// </summary>
public class HandtrackingDemoManager : MonoBehaviour
{
    [Header("Instruction Components")]
    [SerializeField]
    [Tooltip("Text object for information")]
    private TextMeshProUGUI textUI;
    [SerializeField]
    private string startingText = "Press the button to let the Ducks out!";
    [SerializeField]
    private string endText = "All Ducks Stored!";
    [SerializeField]
    [Tooltip("Duck reset button object")]
    private GameObject duckButton;

    

    [Header("Starting Duck positions")]
    [SerializeField]
    private Transform duckPos1;
    [SerializeField]
    private Transform duckPos2;
    [SerializeField]
    private Transform duckPos3;

    [Header("Ducks")]
    [SerializeField]
    private GameObject duck1;
    [SerializeField]
    private GameObject duck2;
    [SerializeField]
    private GameObject duck3;

    private int maxDucks = 3;
    private Rigidbody duck1Rb;
    private Rigidbody duck2Rb;
    private Rigidbody duck3Rb;

    

    private int duckCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetUpStart();
    }

    private void OnTriggerEnter(Collider other)
    {
        //When a duck object moves to the box, check if it is the correct Duck object
        if (other.CompareTag("Duck"))
        {
            other.gameObject.SetActive(false);
            SoundManager.instance.PlayDuck();

            //Keep count to know when all ducks are collected
            duckCount++;
            UpdateText(duckCount);
            if (duckCount >= maxDucks)
                SetUpEndText();
        }
        else if (other.CompareTag("Grabbable")) //Play an error message if its the wrong item
            SoundManager.instance.PlayError();
    }

    /// <summary>
    /// Set up the information text and cache the rigidbodies
    /// </summary>
    private void SetUpStart()
    {
        textUI.text = startingText;

        duck1Rb = duck1.GetComponent<Rigidbody>();
        duck2Rb = duck2.GetComponent<Rigidbody>();
        duck3Rb = duck3.GetComponent<Rigidbody>();

        duck1.SetActive(false);
        duck2.SetActive(false);
        duck3.SetActive(false);
        
    }

    private void UpdateText(int duckCount)
    {
        textUI.text = $"Ducks Stored: {duckCount}";
    }

    /// <summary>
    /// Reset the duck positions and their velocity
    /// </summary>
    public void ResetDucks()
    {
        duck1.transform.position = duckPos1.position;
        duck1.transform.rotation = duckPos1.rotation;
        duck1Rb.velocity = Vector3.zero;
        duck1Rb.angularVelocity = Vector3.zero;
        duck1.SetActive(true);
        duck2.transform.position = duckPos2.position;
        duck2.transform.rotation = duckPos2.rotation;
        duck2Rb.velocity = Vector3.zero;
        duck2Rb.angularVelocity = Vector3.zero;
        duck2.SetActive(true);
        duck3.transform.position = duckPos3.position;
        duck3.transform.rotation = duckPos3.rotation;
        duck3Rb.velocity = Vector3.zero;
        duck3Rb.angularVelocity = Vector3.zero;
        duck3.SetActive(true);

        duckCount = 0;
        UpdateText(duckCount);
        SoundManager.instance.PlayDuckStart();
    }

    private void SetUpEndText()
    {
        textUI.text = endText;
        SoundManager.instance.PlayDuckEnd();
    }
}
