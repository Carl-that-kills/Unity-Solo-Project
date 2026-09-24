using UnityEngine;

public class Enemy : EnemyBehavior
{

    public WeaponsBehavior weapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Damager").GetComponent<WeaponsBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Damager")
        {
            EnemyHealth = EnemyHealth - weapon.weaponDMG;
        }
    }
}
