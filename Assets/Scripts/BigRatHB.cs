using UnityEngine;
using UnityEngine.UI;

public class BigRatHB : MonoBehaviour
{
    public GameObject healthBar;
    public Slider healthBarSlider;
    public Image healthBarFill;

    public EnemyObject enemy;

    void FixedUpdate()
    {
        healthBarSlider.value = enemy.health;
        healthBarFill.color = Color.Lerp(Color.red, Color.green, enemy.health / enemy.enemyType.maxHealth);
    }

    public void Show()
    {
        healthBar.SetActive(true);
    }

    public void Close()
    {
        healthBar.SetActive(false);
    }
}
