using System.Collections;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

/// <summary>
/// Controls the logic of the Scene Selection Window
/// </summary>
public class SceneControl : MonoBehaviour
{
    private static SceneControl _instance;
    public static SceneControl instance
    {
        get => _instance;
        private set
        {
            if (_instance != null)
                Debug.LogWarning("Second attempt to get SceneControl");
            _instance = value;
        }
    }

    [SerializeField]
    [Tooltip("Controls the black screen fade in an dout")]
    private PXR_ScreenFade screenFade;// Control Black screen
    [SerializeField]
    [Tooltip("Animator of the menu scene selector")]
    private Animator menuAnim;
    [SerializeField]
    [Tooltip("Button Action Reference")]
    private InputActionReference sceneMenuButton;
    [SerializeField]
    [Tooltip("")]
    private GameObject sceneMenuObject;
    [SerializeField]
    [Tooltip("Menu offset placement")]
    private Vector3 menuOffset = new Vector3(0, 0, 3);
    [SerializeField]
    [Tooltip("Delay of the window opening and closing")]
    private float menuDelay = 3f;
    [SerializeField]
    [Tooltip("Text of the current scene for the menu")]
    private TextMeshProUGUI currentSceneTextUi;

    private float currTime;
    private Transform player;
    private bool menuOpen = false;

    private void Awake()
    {
        instance = this;
        sceneMenuButton.action.started += OnPrimaryPressed;
        player = Camera.main.transform;
        sceneMenuObject.transform.SetParent(player);
        sceneMenuObject.transform.localPosition = menuOffset;
        sceneMenuObject.transform.localRotation = Quaternion.identity;
        currentSceneTextUi.text = SceneManager.GetActiveScene().name;
    }
    // Start is called before the first frame update
    void Start()
    {
        currTime = 0;
    }


    private void OnDestroy()
    {
        sceneMenuButton.action.started -= OnPrimaryPressed;

    }

    private void Update()
    {
        if(menuOpen)
        {
            currTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// When menu button is pressed on left or right controller
    /// </summary>
    /// <param name="cb"></param>
    public void OnPrimaryPressed(InputAction.CallbackContext cb)
    {
        menuOpen = !menuOpen;
        MenuToggle(menuOpen);
    }

    /// <summary>
    /// Menu logic
    /// </summary>
    /// <param name="menuStatus"></param>
    public void MenuToggle(bool menuStatus)
    {
        if (menuOpen)
        {
            menuAnim.SetTrigger("MenuOpen");
        }
        else
        {
            menuAnim.SetTrigger("MenuClose");
        }
    }

    public void MenuOn()
    {
        menuOpen = true;
        MenuToggle(menuOpen);
    }

    public void MenuOff()
    {
        if (currTime < menuDelay)
            return;

        currTime= 0;
        menuOpen = false;
        MenuToggle(menuOpen);
    }

    /// <summary>
    /// Run the coroutine
    /// </summary>
    /// <param name="sceneName"></param>
    public void COStartSceneLoad(string sceneName)
    {
        if(sceneName == null || sceneName == string.Empty)
        {
            Debug.LogWarning("Scene name empty");
            return;
        }

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    /// <summary>
    /// Coroutine to load a scene
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            screenFade.SetCurrentAlpha(1f);
            yield return null;
        }

    }
}
