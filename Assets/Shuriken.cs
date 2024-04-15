using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.DecrementHealth(5);
            Destroy(gameObject);
        }

        
    }


}
