using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Health : MonoBehaviour
{

    // delegate signature de fonction
    public delegate void HealthEvent();

    //Listeners
    public HealthEvent OnHpChanged;
    public HealthEvent OnMaxHpChanged;
    public HealthEvent OnHurt;
    public HealthEvent OnHeal;
    public HealthEvent OnDeath;
    public HealthEvent OnStopInvincible;

    [Range(0, 60)]
    public float InvincibleTime = 1f;
    private bool _isInvincible = false;

    [SerializeField, Range(1, 50)]
    private int _maxHp = 1;
    public int MaxHp
    {
        get { return _maxHp; }
        set
        {
            if (_maxHp != value)
            {
                _maxHp = value;
                OnMaxHpChanged?.Invoke();
            }
        }
    }

    [SerializeField, Range(0, 50)]
    private int _hp = 1;
    public int Hp
    {
        get { return _hp; }
        set
        {
            int temp = _hp;
            _hp = Mathf.Clamp(value, 0, MaxHp);
            if (_hp != temp)
            {
                OnHpChanged?.Invoke();

                if (_hp <= 0)
                {
                    OnDeath?.Invoke();
                }
            }
        }
    }

    private void Awake()
    {
        Hp = Hp;
    }

    public int Hurt(int damageValue)
    {
        if (!_isInvincible)
        {
            damageValue = Mathf.Max(damageValue, 0);
            Hp = Mathf.Max(Hp - damageValue, 0);

            if (damageValue > 0)
            {
                OnHurt?.Invoke();
                WaitInvincibleFrames(InvincibleTime);
            }
        }

        return Hp;
    }

    public int Heal(int healValue)
    {
        healValue = Mathf.Max(healValue, 0);
        Hp = Mathf.Min(Hp + healValue, MaxHp);

        if (healValue > 0)
        {
            OnHeal?.Invoke();
        }

        return Hp;
    }

    public async void WaitInvincibleFrames(float duration)
    {
        _isInvincible = true;

        float end = Time.time + duration;

        while (Time.time < end)
        {
            await Task.Yield();
        }

        _isInvincible = false;
        OnStopInvincible?.Invoke();
    }
}
