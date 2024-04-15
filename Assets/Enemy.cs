using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health = 20;

    public void DecrementHealth(int value)
    {
        Health -= value;
        if(Health < 0)
        {
            Health = 0;
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
