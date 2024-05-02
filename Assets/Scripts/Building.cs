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
    
    private void HealthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
}
