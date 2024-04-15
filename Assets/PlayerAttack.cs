using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject shuriken;
    public GameObject smuga;
    public float throwForce;
    public Enemy target;

    Queue<KeyCode> recentInputs = new Queue<KeyCode>();

    public List<KeyCode> meleeCombo = new List<KeyCode>();
    public List<KeyCode> shurikenCombo = new List<KeyCode>();

    private void Update()
    {
        CaputereKey();
        DetectCombo(meleeCombo, AttackMelee);
        DetectCombo(shurikenCombo, ThrowShuriken);
    }

    private void DetectCombo(List<KeyCode> combo, Action behavior)
    {
        if (recentInputs.Count >= combo.Count)
        {
            bool comboMatched = true;
            int index = recentInputs.Count - combo.Count;
            for (int i = 0; i < combo.Count; i++)
            {
                if (recentInputs.ToArray()[index + i] != combo[i])
                {
                    comboMatched = false;
                    break;
                }
            }
            if (comboMatched)
            {
                behavior?.Invoke();
                recentInputs.Clear();
            }
        }
    }

    private void CaputereKey()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            recentInputs.Enqueue(KeyCode.DownArrow);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            recentInputs.Enqueue(KeyCode.UpArrow);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            recentInputs.Enqueue(KeyCode.RightArrow);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            recentInputs.Enqueue(KeyCode.LeftArrow);
        }
    }

    public void AttackMelee()
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(smuga, transform.position, Quaternion.identity);

        // Calculate direction towards the target
        Vector2 direction = (target.transform.position - transform.position).normalized;
        float moveDistance = 2.5f;
        Vector2 newPosition = (Vector2)projectile.transform.position + direction * moveDistance;
        projectile.transform.position = newPosition;
    }



    public void ThrowShuriken()
    {
        GameObject projectile = Instantiate(shuriken, transform.position, Quaternion.identity);
        Vector2 direction = (target.transform.position - transform.position).normalized;
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        rbProjectile.AddForce(direction * throwForce, ForceMode2D.Impulse);
    }

}
