using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour , IHealth
{
    public float _maxHealth;
    public float _currentHealth;

    public float maxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }
    public float currentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }


    private void Start()
    {
        
    }
    private void OnEnable()
    {
        EventManager.HealthsEvent.TakeDamege.AddListener(TakeDamage);
    }
    private void OnDisable()
    {
        EventManager.HealthsEvent.TakeDamege.RemoveListener(TakeDamage);
    }

    public void TakeDamage(float damageValue)
    {
        throw new System.NotImplementedException();
    }
    public void UnitOnDie()
    {
        throw new System.NotImplementedException();
    }

}
