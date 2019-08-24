using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    public float playerSpeed = 3f;
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
        Vector3 rightMovement = transform.right * horizInput;

        characterController.SimpleMove(forwardMovement + rightMovement);
        rb.AddForce(Vector3.up * jumpForce);
    }
}
