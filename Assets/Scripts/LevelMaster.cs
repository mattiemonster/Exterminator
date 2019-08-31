using UnityEngine;

public class LevelMaster
{
    public static bool movementTutCompleted = false;
    public static bool shootTutCompleted = false;
    public static bool reloadTutCompleted = false;
    public static bool noRatSpawning = false;
    public static bool noAcidRatSpawning = false;
    public static bool viewedIntro = false;

    public static LevelMaster prime;

    public BigRatHB bossHB;

    public static void Start()
    {
        prime = new LevelMaster();
        prime.bossHB = GameObject.FindWithTag("HealthbarObject").GetComponent<BigRatHB>();
        prime.bossHB.gameObject.SetActive(false);
    }
}
