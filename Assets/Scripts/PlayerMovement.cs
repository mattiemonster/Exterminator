using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    public float playerSpeed = 12f;
    public float jumpForce = 2f;
    public bool acceptInput = true;

    private CharacterController characterController;
    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!acceptInput) return;

        float horizInput = Input.GetAxis("Horizontal") * playerSpeed;
        float vertInput = Input.GetAxis("Vertical") * playerSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 horizontalMovement = transform.right * horizInput;
        Vector3 movement = forwardMovement + horizontalMovement;

        characterController.SimpleMove(movement);
    }

    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
    }
}
