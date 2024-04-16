
using UnityEngine;

public enum EnemyState
{
    Patroling,
    Triggered,
}
// Pozdro

public class Enemy : MonoBehaviour
{
    public Transform player;
    public int Health = 20;
    public float triggerRange = 5;
    public float patrolSpeed = 5;
    public float patrolingRange = 3;
    public float attackRange = 3;

    public EnemyState currentState = EnemyState.Patroling;
    bool isMoving = false;
    Vector3 startingPosition;
    Vector2 destination;

    private void Start()
    {
        startingPosition = transform.position;
    }
    private void SetNewDestination()
    {
        destination = new Vector2(Random.Range(startingPosition.x - patrolingRange, startingPosition.x + patrolingRange), Random.Range(startingPosition.y - patrolingRange, startingPosition.y + patrolingRange));
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patroling:
                Patroling();
                break;
            case EnemyState.Triggered:
                Triggered();
                break;
        }
    }

    private void Triggered()
    {
        Debug.Log("Triggered");
        
        if (Vector2.Distance(transform.position, player.position) < attackRange)
        {
            Attack();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, patrolSpeed * Time.deltaTime);
        }
    }

    private void Patroling()
    {
        if (player != null && Vector3.Distance(player.position, transform.position) <= triggerRange)
        {
            isMoving = false;
            currentState = EnemyState.Triggered;
            return;
        }

        if (!isMoving)
        {
            SetNewDestination();
            isMoving = true;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, patrolSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, destination) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Attacking the player!");
    }

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, triggerRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(startingPosition, new Vector3(patrolingRange * 2, patrolingRange * 2, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
