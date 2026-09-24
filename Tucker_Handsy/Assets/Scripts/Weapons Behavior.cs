using System.Collections;
using UnityEngine;

public class WeaponsBehavior : MonoBehaviour
{
    PlayerController player;

    [Header("Object Refrences")]
    public GameObject projectile;
    public Transform firePoint;
    public Camera firingDirection;

    [Header("Meta Attributes")]
    public bool canAttack = true;
    public bool holdToAttack = true;
    public bool reloading = false;
    public int weaponID;
    public string weaponName;

    [Header("Weapon Stats")]
    public string weaponType;
    public int weaponDMG;
    //Melee Stats
    public float BluntSwing;
    public float BluntSpeed;
    public float BluntDelay;
    //Gun Stats
    public float projLifespan;
    public float projVelocity;
    public float reloadCooldown;
    public float rof;
    public int fireModes;
    public int currentFireMode;
    public int clip;
    public int clipSize;

    [Header("Ammo Stats")]
    public int ammo;
    public int maxAmmo;
    public int ammoRefill;

/*
    [Header("Weapon Invitory")]
    //public List<int> CollectedWeaponsID = new List<int>();
*/
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        firePoint = transform.GetChild(0);
        firingDirection = Camera.main;
    }

    public void equip(PlayerController p)
    {
        player = p;

        player.currentWeapon = this;

        transform.SetPositionAndRotation(player.weaponslot.position, player.weaponslot.rotation);
        transform.SetParent(player.weaponslot);

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
    }

    public void unequip()
    {
        player.currentWeapon = null;

        transform.SetParent(null);

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().isTrigger = false;

        player = null;
    }

    public void reload()
    {
        if (weaponType == "Gun")
        {
            if (clip >= clipSize)
                return;

            int reloadCount = clipSize - clip;

            if (ammo < reloadCount)
            {
                clip += ammo;
                ammo = 0;
            }
            else
            {
                clip += reloadCount;
                ammo -= reloadCount;
            }

            reloading = true;
            canAttack = false;
            StartCoroutine("reloadingCooldown");
        }
    }

    public void fire()
    {
        if (clip > 0 && canAttack && !reloading)
        {
            clip--;

            GameObject p = Instantiate(projectile, firePoint.position, firePoint.rotation);
            p.GetComponent<Rigidbody>().AddForce(firingDirection.transform.forward * projVelocity);
            Destroy(p, projLifespan);
            canAttack = false;
            StartCoroutine("cooldownFire");
        }
    }

    IEnumerator reloadingCooldown()
    {
        yield return new WaitForSeconds(reloadCooldown);

        reloading = false;
        canAttack = true;
    }

    IEnumerator cooldownFire()
    {
        yield return new WaitForSeconds(rof);

        if (clip > 0)
            canAttack = true;
    }


}
