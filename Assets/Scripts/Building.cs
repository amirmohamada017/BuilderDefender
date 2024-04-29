using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem _healthSystem;
    
    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _healthSystem.Damage(30);
            Debug.Log(_healthSystem.GetHealthAmount());
            Debug.Log(_healthSystem.GetHealthAmountNormalized());
            Debug.Log(_healthSystem.IsDead());
        }
    }

    private void HealthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
}
