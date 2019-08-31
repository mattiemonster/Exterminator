using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject endCover;

    public void End()
    {
        StartCoroutine(Ending());
    }

    IEnumerator Ending()
    {
        yield return new WaitForSeconds(3.5f);
        endCover.SetActive(true);
        endCover.GetComponent<Animator>().Play("EndCover");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EndingText");
    }
}
