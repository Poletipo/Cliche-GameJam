using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHitter : MonoBehaviour {

    public Action OnHit;

    [SerializeField] bool _isActivated = false;
    public bool NeedRefresh = false;
    public float RefreshTimer = 1;
    public float KnockbackForce = 10;
    public float KnockbackTime = 1;
    public GameObject HitterSource;
    public AudioClip HitSFX;


    private float _refreshTimeStart;
    private List<Collider> collidersList = new List<Collider>();

    private void Start() {
        _refreshTimeStart = Time.time;
    }

    private void Update() {
        if (NeedRefresh) {
            if (RefreshTimer + _refreshTimeStart < Time.time) {
                collidersList.Clear();
                _refreshTimeStart = Time.time;
            }
        }
    }

    public void Activate() {
        _isActivated = true;
    }

    public void Deactivate() {
        _isActivated = false;
        collidersList.Clear();
    }

    private void OnTriggerStay(Collider collider) {
        if (_isActivated) {

            IDamageable hitable = collider.GetComponent<IDamageable>();

            if (hitable != null && !collidersList.Contains(collider)) {

                collidersList.Add(collider);
                Hit(hitable);
            }
        }

    }

    private void Hit(IDamageable hitable) {
        IDamageable.HitterValue value = new IDamageable.HitterValue() {
            dmg = 1,
            hitter = this,
            force = KnockbackForce,
            knockTime = KnockbackTime
        };

        bool isHit = hitable.Hit(value);

        if (isHit) {
            if (HitSFX != null) {
                AudioManager.Instance.PlayAudio(HitSFX, transform.position, .25f);
            }
            OnHit?.Invoke();
        }

    }

}
