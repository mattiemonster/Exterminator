using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Scene References")]
    public GameObject playerObject;

    [Header("Values")]
    public float heightOffset;

    void Update()
    {
        Vector3 newPos = playerObject.transform.position;
        newPos.y += heightOffset;
        transform.position = newPos;
    }
}
