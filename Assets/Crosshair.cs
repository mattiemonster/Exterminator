using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public void PlayDamageAnim()
    {
        GetComponent<Animator>().Play("CrosshairDamageAnim");
    }
}
