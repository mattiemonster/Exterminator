using UnityEngine;

public class ShootTutorial : MonoBehaviour
{
    bool listen;

    public void ShowTutorial()
    {
        GetComponent<Animator>().Play("OpenShoot");
        listen = true;
    }

    void Update()
    {
        if (listen && Input.GetButtonDown("PrimaryAttack"))
        {
            GetComponent<Animator>().Play("CloseShoot");
            LevelMaster.shootTutCompleted = true;
            Destroy(gameObject, 1.75f);
        }
    }
}
