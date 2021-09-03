using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healtRestoreRate;
   

    [SerializeField] private float chasingRange;
    [SerializeField] private float shootingRange;
    [SerializeField] private Transform playerTransform;


    private float currentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, startingHealth); }
    }
    void Start()
    {
        currentHealth = startingHealth;
    }
    void Update()
    {
        currentHealth += Time.deltaTime * healtRestoreRate;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

}
