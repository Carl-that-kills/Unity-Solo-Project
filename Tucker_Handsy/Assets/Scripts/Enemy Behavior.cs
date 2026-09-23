using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{

    public bool isFollowing = false;

    public int EnemyDMG;
    public int EnemyCooldown;
    public bool EnemyCanAttack = true;
    public bool EnemyWantsToAttack = false;
    public bool PlayerVunerable = false;

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
        if (isFollowing == true)
        {
            agent.destination = player.transform.position;
        }

        if (EnemyWantsToAttack == true)
        {
            if (EnemyCanAttack == true)
            {
                player.Health = player.Health - EnemyDMG;
                StartCoroutine("EnemyCoolDown");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isFollowing = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isFollowing = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isFollowing = false;
            PlayerVunerable = true;
            EnemyWantsToAttack = true;
            EnemyCanAttack = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isFollowing = true;
            EnemyWantsToAttack = false;
            PlayerVunerable = false;

        }
    }
    IEnumerator EnemyCoolDown()
    {
        EnemyCanAttack = false;

        yield return new WaitForSeconds(EnemyCooldown);
        if(PlayerVunerable == true)
        {
            EnemyCanAttack = true;
        }
    }

}
