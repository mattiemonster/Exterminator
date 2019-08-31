using UnityEngine;
using UnityEngine.SceneManagement;

public class EscForMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Menu");
        }
    }
}
