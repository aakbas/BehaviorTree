using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healtRestoreRate;
    private float currentHealth;

    private float CurrenHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth); }
    }
    void Start()
    {
        currentHealth = startingHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

}
