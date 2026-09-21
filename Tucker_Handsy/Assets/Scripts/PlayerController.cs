using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int Health = 10;
    public int maxHealth = 10;

    public float speed = 5.0f;
    public float jumpHeight = 10.0f;
    public float jumpDectectionHeight = 1.0f;
    public float interactDistince;
    public float fusionDmgInterval;

    public bool attacking = false;
    public bool fusionDmg = false;

    PlayerInput input;
    public Transform weaponslot;
    Rigidbody rb;
    Camera playerCam;
    public GameObject pickupObject;

    public WeaponsBehavior currentWeapon;

    Ray jumpRay;
    Ray interactRay;
    RaycastHit interactHit;
    Vector2 moveInput = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        jumpRay = new Ray();
        playerCam = Camera.main;

        interactRay = new Ray();
        weaponslot = playerCam.transform.GetChild(0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Quaternion playerRotaition = Quaternion.identity;
        playerRotaition.y = playerCam.transform.rotation.y;
        playerRotaition.w = playerCam.transform.rotation.w;
        transform.rotation = playerRotaition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {

        }

        jumpRay.origin = transform.position;
        jumpRay.direction = -transform.up;

        interactRay.origin = playerCam.transform.position;
        interactRay.direction = playerCam.transform.forward;

        if (Physics.Raycast(interactRay, out interactHit, interactDistince))
        {
            if (interactHit.collider.tag == "Weapons")
            {
                pickupObject = interactHit.collider.gameObject;
            }
            else
                pickupObject = null;
        }
        else
            pickupObject = null;

        if (currentWeapon)
            if (currentWeapon.holdToAttack && attacking)
                currentWeapon.fire();

        Vector3 tempMove = rb.linearVelocity;

        tempMove.x = moveInput.x * speed;
        tempMove.z = moveInput.y * speed;

        rb.linearVelocity = (tempMove.x * transform.right)+
                            (tempMove.y * transform.up)+
                            (tempMove.z * transform.forward);

    }

    public void move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void jump()
    {
        if(Physics.Raycast(jumpRay, jumpDectectionHeight))
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
    }

    public void interact(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            if (pickupObject)
            {
                if (pickupObject.tag == "Weapons")
                {
                   pickupObject.GetComponent<WeaponsBehavior>().equip(this);
                }

                pickupObject = null;
            }
            else if (currentWeapon)
                Reload();
        }
    }

    public void Reload()
    {
        if (currentWeapon)
            if (!currentWeapon.reloading)
                currentWeapon.reload();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (currentWeapon)
        {
            if (currentWeapon.holdToAttack)
            {
                if (context.ReadValueAsButton())
                    attacking = true;
                else
                    attacking = false;
            }

            else if (context.ReadValueAsButton())
                currentWeapon.fire();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            Health--;
        }

        if (collision.gameObject.tag == "FusionHazard")
        {
            Health--;
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "FusionHazard")
        {
            if (!fusionDmg)
            {
                StartCoroutine("fusionDmgCooldown");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LevelEnd")
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "FusionHazard")
        {
            if (fusionDmg)
            {
                StopCoroutine("fusionDmgCooldown");
                fusionDmg = false;
            }
        }
    }



    IEnumerator fusionDmgCooldown()
    {
        fusionDmg = true;

        yield return new WaitForSeconds(fusionDmgInterval);

        Health--;
        fusionDmg = false;
    }

}