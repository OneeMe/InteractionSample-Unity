using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameFollower : MonoBehaviour
{

    [SerializeField]
    private GameObject flame;

    [SerializeField]
    private Transform flameFollower;
    [SerializeField]
    [Tooltip("Web shooting sound clip")]
    private AudioClip shootClip;

    void Start()
    {
        TurnOffFlame();
        flame.AddComponent<AudioSource>();
        AudioSource audioSource = flame.GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = shootClip;
    }

    // Update is called once per frame
    void Update()
    {
        flame.transform.position = flameFollower.position;
    }

    public void ToggleFlame()
    {
        if (flame.activeSelf)
        {
            TurnOffFlame();
        }
        else
        {
            TurnOnFlame();
        }
    }

    public void TurnOnFlame()
    {
        Debug.Log("TurnOnFlame");
        flame.SetActive(true);
        flame.GetComponent<AudioSource>().Play();
    }

    public void TurnOffFlame()
    {
        Debug.Log("TurnOffFlame");
        flame.SetActive(false);
        flame.GetComponent<AudioSource>().Stop();
    }
}

