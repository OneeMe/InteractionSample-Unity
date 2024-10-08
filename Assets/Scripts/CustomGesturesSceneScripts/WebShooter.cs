using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to fire an object with Rigidbody by using AddForce and and audio sound
/// </summary>
public class WebShooter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Prefab of your web ball with a trail renderer")]
    private GameObject webPrefab;
    [SerializeField]
    [Tooltip("Transform of where the ball is coming from")]
    private Transform shootPoint;
    [SerializeField]
    [Tooltip("Force of applied to the rigidbody of the ball")]
    private float shootForce = 15f;
    [SerializeField]
    [Tooltip("Audiosource for sound")]
    private AudioSource audiSource;
    [SerializeField]
    [Tooltip("Web shooting sound clip")]
    private AudioClip shootClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Called when the Webshoot gesture is performed to instantiate the web ball and apply force to it with an audio cue
    /// </summary>
    public void OnWebShoot()
    {
        GameObject newWeb = Instantiate(webPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = newWeb.GetComponent<Rigidbody>();
        if(rb)
        {
            //physics to move the web ball
            rb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
            //play audio when you shoot the web
            audiSource.PlayOneShot(shootClip);
        }
    }
}
