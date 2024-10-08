using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Create and populate button list of scenes for Scene selection window
/// </summary>
public class SceneButtonCreate : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab; // Assign a UI Button prefab
    [SerializeField]
    private Transform buttonParent; // Assign the parent transform where buttons will be instantiated

    private void Awake()
    {
        MakeSceneButtons();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void MakeSceneButtons()
    {
        if (buttonParent == null || buttonParent == null)
            return;

        Scene currScene = SceneManager.GetActiveScene();
        int currIndex = currScene.buildIndex; 

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (currIndex == i)
                continue;//Skip making button for current Scene

            // Create a new button
            GameObject newButton = Instantiate(buttonPrefab, buttonParent);

            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            Debug.Log($"Scene {i}: {sceneName}");
            SceneButtonControl sbctrl = newButton.GetComponent<SceneButtonControl>();
            if (!sbctrl)
                newButton.AddComponent<SceneButtonControl>();

            sbctrl.UpdateText(sceneName);
        }
    }
}
