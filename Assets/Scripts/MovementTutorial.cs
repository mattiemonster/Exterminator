using UnityEngine;
using TMPro;

public class MovementTutorial : MonoBehaviour
{
    [Header("Scene References")]
    public TextMeshProUGUI wUI;
    public TextMeshProUGUI aUI, sUI, dUI;

    [Header("Values")]
    public Color pressedColour = Color.green;

    private int progress = 0;
    private bool w, a, s, d;

    void Start()
    {
        GetComponent<Animator>().Play("OpenWASD");
    }

    void Update()
    {
        if (!w && Input.GetKeyDown(KeyCode.W))
        {
            wUI.color = pressedColour;
            progress++;
            w = true;
        }

        if (!a && Input.GetKeyDown(KeyCode.A))
        {
            aUI.color = pressedColour;
            progress++;
            a = true;
        }

        if (!s && Input.GetKeyDown(KeyCode.S))
        {
            sUI.color = pressedColour;
            progress++;
            s = true;
        }

        if (!d && Input.GetKeyDown(KeyCode.D))
        {
            dUI.color = pressedColour;
            progress++;
            d = true;
        }

        if (progress == 4)
        {
            LevelMaster.movementTutCompleted = true;
            GetComponent<Animator>().Play("CloseWASD");
            Destroy(gameObject, 0.8f);
        }
    }
}
