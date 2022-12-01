using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHitter : MonoBehaviour
{

    public AudioClip hitClip;

    public Action OnHit;

    public bool needRefresh = false;
    public float RefreshTimer = 1;
    public float knockbackForce = 10;
    public float knockbackTime = 1;
    private float _refreshTimeStart;

    public GameObject HitterSource;

    [SerializeField]
    bool _isActivated = false;
    List<Collider> collidersList = new List<Collider>();


    private void Start()
    {
        _refreshTimeStart = Time.time;
    }


    private void Update()
    {
        if (needRefresh)
        {
            if(RefreshTimer + _refreshTimeStart < Time.time)
            {
                collidersList.Clear();
                _refreshTimeStart = Time.time;
            }
        }
    }


    public void Activate()
    {
        _isActivated = true;
    }

    public void Deactivate()
    {
        _isActivated = false;
        collidersList.Clear();
    }

    private void OnTriggerStay(Collider collider)
    {
        if (_isActivated)
        {

            IHitable hitable = collider.GetComponent<IHitable>();

            if(hitable != null && !collidersList.Contains(collider))
            {

                collidersList.Add(collider);
                Hit(hitable);

            }
        }

    }

    private void Hit(IHitable hitable)
    {
        IHitable.HitterValue value = new IHitable.HitterValue();
        value.dmg = 1;
        value.hitter = this;
        value.force = knockbackForce;
        value.knockTime = knockbackTime;

        bool isHit = hitable.Hit(value);

        if (isHit)
        {
            if(hitClip != null)
            {
                AudioManager.Instance.PlayAudio(hitClip, transform.position, .25f);
            }
            OnHit?.Invoke();
        }

    }

}
