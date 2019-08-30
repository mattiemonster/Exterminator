using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public TextTyper textTyper;
    public GameObject textObject;
    public GameObject loadingDialogObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!textTyper.AnyEntriesLeft)
            {
                loadingDialogObject.SetActive(true);
                SceneManager.LoadSceneAsync("Level");
                return;
            }

            textTyper.LoadNextString();
            textObject.GetComponent<Animator>().StopPlayback();
            textObject.GetComponent<Animator>().Play("ContinueAnim");
        }
    }
}
