using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;
    
    [SerializeField] private int healthAmountMax;
    
    private int _healthAmount;

    private void Awake()
    {
        _healthAmount = healthAmountMax;
    }

    public void Damage(int damageAmount)
    {
        _healthAmount -= damageAmount;
        _healthAmount = Mathf.Clamp(_healthAmount, 0, healthAmountMax);
        
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (IsDead())
            OnDied?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        return _healthAmount == 0;
    }

    public int GetHealthAmount()
    {
        return _healthAmount;
    }

    public float GetHealthAmountNormalized()
    {
        return (float)_healthAmount / healthAmountMax;
    }
}
