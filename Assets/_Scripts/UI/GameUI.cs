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
    Transform _healthContainer;
    [SerializeField]
    GameObject _heartSprite;
    [SerializeField]
    TextMeshProUGUI keyCountTxt;
    bool lastHeart = false;
    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.Instance.Player;
        _health = _player.GetComponent<Health>();
        _health.OnHurt += OnHurt;
        _health.OnHeal += OnHeal;

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


    // Update is called once per frame
    void Update()
    {
        
    }
}
