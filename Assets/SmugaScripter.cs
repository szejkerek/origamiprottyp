using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmugaScripter : MonoBehaviour
{
    public float timer = 0.75f;

    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Col");
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.DecrementHealth(10);
        }
    }
}
