using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{

    public bool isFollowing = false;

    public float EnemyDMG;
    public int EnemyCooldown;
    public bool EnemyCanAttack = false;
    public bool EnemyAttacking = false;

    public NavMeshAgent agent;
    public PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            agent.destination = player.transform.position;
        }
        if (EnemyCanAttack == true)
        {
            EnemyCanAttack = false;
            EnemyAttacking = true;
            
            player.Health--;

            StartCoroutine("EnemyCoolDown");
        } else
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player");
        {
            isFollowing = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player");
        {
            isFollowing = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isFollowing = false;

            EnemyCanAttack = true;
            
            // Move if they have to get back to player, if collider isnt in collider move

            // Set attacking boolean to true
            // In update, make enemy attack and do damage to player while attacking
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isFollowing = true;
            EnemyCanAttack = false;
            EnemyAttacking = false;
        }
    }

    // Run a coroutine for a cooldown so the enemy doesn't keep trying to attack the player every instance
    IEnumerator EnemyCoolDown()
    {
        yield return new WaitForSeconds(EnemyCooldown);

        if (EnemyAttacking == true)
        {
            EnemyCanAttack = true;
        }
    }
}
