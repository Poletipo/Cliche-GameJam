using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{

    GameObject _player;
    PlayerController _playerCtrl;
    Health _health;


    [SerializeField]
    Animation NextLevel_Anim;
    [SerializeField]
    Transform _healthContainer;
    [SerializeField]
    GameObject _heartSprite;
    [SerializeField]
    TextMeshProUGUI keyCountTxt;
    [SerializeField]
    GameObject _deathScreen;
    [SerializeField]
    GameObject _pauseScreen;
    [SerializeField]
    GameObject _winScreen;
    bool lastHeart = false;
    private Animation anim;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.Instance.Player;
        _health = _player.GetComponent<Health>();
        _health.OnHurt += OnHurt;
        _health.OnHeal += OnHeal;
        _health.OnDeath += OnDeath;

        _playerCtrl = _player.GetComponent<PlayerController>();

        _playerCtrl.OnKeyCountChanged += OnKeyCountChanged;



        if(_healthContainer.childCount > 0)
        {
            Destroy(_healthContainer.GetChild(0).gameObject);
        }

        for (int i = 0; i < _health.Hp; i++)
        {
            SpawnHeartSprite();
        }

    }

    private void OnDeath()
    {
        StartCoroutine(OnDeathCoroutine());
    }

    private IEnumerator OnDeathCoroutine()
    {
        yield return new WaitForSeconds(1);
        _deathScreen.SetActive(true);
    }

    public void Restart()
    {
        GameManager.Instance.RestartLevel();
    }

    public void StartOver()
    {
        GameManager.Instance.LoadLevel(0);
    }

    private void OnKeyCountChanged()
    {
        keyCountTxt.text = _playerCtrl.Keycount.ToString();
    }

    private void OnHeal()
    {

        if (lastHeart)
        {
            anim.Play("Heart_Idle");
            lastHeart = false;
        }

        SpawnHeartSprite();
    }

    private void OnHurt()
    {
        if(_healthContainer.childCount == 2)
        {
            anim = _healthContainer.GetChild(1).GetComponentInChildren<Animation>();
            anim.Play("Heart_Last");
            lastHeart = true;
        }
        if(_healthContainer.childCount > 0)
        {
            Destroy(_healthContainer.GetChild(0).gameObject);
        }
    }


    void SpawnHeartSprite()
    {
        Instantiate(_heartSprite, _healthContainer);
    }


    public void NextLevelTransition()
    {
        NextLevel_Anim.Play("Level_End");
    }

    public void Pause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            _pauseScreen.SetActive(true);
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            _pauseScreen.SetActive(false);
            isPaused = false;
        }
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

    public void WinGameScreen()
    {
        _winScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
