using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public TextTyper textTyper;
    public GameObject textObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            textTyper.LoadNextString();
            textObject.GetComponent<Animator>().StopPlayback();
            textObject.GetComponent<Animator>().Play("ContinueAnim");
        }
    }
}
