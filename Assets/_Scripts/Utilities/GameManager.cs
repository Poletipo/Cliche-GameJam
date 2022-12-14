using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager _instance = null;

    private void Awake() {
        if (_instance == null) {
            Instance.Initialize();
        }
    }

    public static GameManager Instance {
        get {
            if (_instance == null) {

                GameObject gmObject = GameObject.Find("GameManager");

                if (gmObject == null) {
                    GameObject gameManagerGameObject = Resources.Load<GameObject>("GameManager");
                    GameObject managerObject = Instantiate(gameManagerGameObject);
                    _instance = managerObject.GetComponent<GameManager>();
                    _instance.Initialize();
                }
                else {
                    _instance = gmObject.GetComponent<GameManager>();
                }

                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    public GameObject Player;
    public GameUI UI;
    public GameObject CameraObject { get; private set; }
    public AudioSource AudioManager { get; private set; }

    private void Initialize() {
        SceneManager.sceneLoaded += OnSceneLoaded;

        OnSceneLoaded();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        OnSceneLoaded();
    }

    private void OnSceneLoaded() {
        Player = GameObject.FindGameObjectWithTag("Player");
        UI = FindObjectOfType<GameUI>();
    }

    public void LoadLevel(int levelNumber) {
        SceneManager.LoadScene(levelNumber);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinGame() {
        UI.WinGameScreen();
    }

    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

}
