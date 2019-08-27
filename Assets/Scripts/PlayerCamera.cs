using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Scene References")]
    public GameObject playerObject;
    public PlayerMovement movementScript;
    public Player playerScript;

    [Header("Values")]
    public float heightOffset = 0.5f;
    public float lookSpeed = 3;

    private Vector2 rotation = new Vector2(0, -90);
    private bool mouseLocked = true;

    void Start()
    {
        MouseLock(true);
    }

    public void MouseLock(bool value)
    {
        switch (value)
        {
            case true:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case false:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }

        movementScript.acceptInput = value;
        playerScript.canShoot = value;
        mouseLocked = value;
    }

    void Update()
    {
        if (mouseLocked)
        {
            Vector3 newPos = playerObject.transform.position;
            // Debug.Log(playerObject.transform.position + ", " + newPos);
            newPos.y += heightOffset;
            transform.position = newPos;

            rotation.y += Input.GetAxis("Mouse X");
            playerObject.transform.eulerAngles = rotation * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y");
            rotation.x = Mathf.Clamp(rotation.x, -30f, 30f);
            transform.eulerAngles = rotation * lookSpeed;
        }

    }
}
