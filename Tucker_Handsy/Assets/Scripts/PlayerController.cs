using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpHeight = 10.0f;
    public float jumpDectectionHeight = 1.0f;

    PlayerInput input;
    Rigidbody rb;
    Camera playerCam;

    Ray jumpRay;
    Vector2 moveInput = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        jumpRay = new Ray();
        playerCam = Camera.main;
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
        jumpRay.origin = transform.position;
        jumpRay.direction = -transform.up;

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
}