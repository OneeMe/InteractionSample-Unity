using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// Used to update the buttons in the Scene menu selection window
/// </summary>
public class SceneButtonControl : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textUi;
    [SerializeField]
    private string sceneName = "";


    private void Awake()
    {
        sceneName = string.Empty;

        if(!textUi)
            textUi = GetComponentInChildren<TextMeshProUGUI>();
        textUi.text= sceneName;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateText(string text)
    {
        textUi.text= text;
        sceneName = text;
    }

    /// <summary>
    /// Triggers when you press the button of the specific scene name on the scene selection window
    /// </summary>
    public void OnButtonClick()
    {
        if (sceneName == null || sceneName == string.Empty)
        {
            Debug.LogWarning("Scene name empty");
            return;
        }

        SceneControl.instance.COStartSceneLoad(sceneName);
    }
}
