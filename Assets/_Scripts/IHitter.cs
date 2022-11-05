using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHitter : MonoBehaviour
{

    public GameObject HitterSource;

    bool _isActivated = false;
    List<Collider> collidersList = new List<Collider>();

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

        hitable.Hit(value);
        Debug.Log($"{hitable.name} is hurt");
    }



}
