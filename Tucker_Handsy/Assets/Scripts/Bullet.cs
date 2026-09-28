using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public WeaponsBehavior weapon;
    public EnemyBehavior Enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapons").GetComponent<WeaponsBehavior>();
        Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy.EnemyHealth = Enemy.EnemyHealth - weapon.weaponDMG;
        }
    }
}
