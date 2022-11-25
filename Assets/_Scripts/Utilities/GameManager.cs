using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance = null;

    private void Awake()
    {
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {

                GameObject gmObject = GameObject.Find("GameManager");

                if (gmObject == null)
                {
                    GameObject gameManagerGameObject = Resources.Load<GameObject>("GameManager");
                    GameObject managerObject = Instantiate(gameManagerGameObject);
                    _instance = managerObject.GetComponent<GameManager>();
                    _instance.Initialize();
                }
                else
                {
                    _instance = gmObject.GetComponent<GameManager>();
                }

                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    [SerializeField]
    public GameObject Player;
    public GameObject CameraObject { get; private set; }
    public AudioSource AudioManager { get; private set; }

    public bool FirstTimeSession = true;

    private void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        OnSceneLoaded();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        OnSceneLoaded();
    }

    private void OnSceneLoaded()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

    }

}
