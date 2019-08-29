using UnityEngine;

public class ReloadTutorial : MonoBehaviour
{
    bool listen;

    public void ShowTutorial()
    {
        GetComponent<Animator>().Play("OpenReload");
        listen = true;
    }

    void Update()
    {
        if (listen && Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<Animator>().Play("CloseReload");
            LevelMaster.reloadTutCompleted = true;
            Destroy(gameObject, 1.75f);
        }
    }
}
