using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Scene References")]
    public GameObject playerObject;

    [Header("Values")]
    public float heightOffset = 0.5f;
    public float lookSpeed = 3;
    Vector2 rotation = new Vector2(0, 0);

    void Update()
    {
        Vector3 newPos = playerObject.transform.position;
        newPos.y += heightOffset;
        transform.position = newPos;

        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -20f, 20f);
        transform.eulerAngles = (Vector2)rotation * lookSpeed;
    }
}
