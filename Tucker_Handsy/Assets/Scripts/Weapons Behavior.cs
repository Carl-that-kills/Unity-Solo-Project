using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponsBehavior : MonoBehaviour
{
    PlayerController player;

    [Header("Object Refrences")]
    public GameObject projectile;
    public Transform firePoint;
    public Camera firingDirection;

    [Header("Meta Attributes")]
    public bool canfire = true;
    public bool holdToAttack = true;
    public bool realoading = false;
    public int weaponID;
    public string weaponName;

    [Header("Weapon Status")]
    public float projLifespan;
    public float projVelocity;
    public float reloadCooldown;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void realod()
    {

    }

    public void fire()
    {

    }

    IEnumerator reloadingCooldown()
    {

    }

    IEumerator fireCooldown()
    {

    }

    IEnumerator 
}
