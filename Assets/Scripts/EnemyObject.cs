using System.Collections;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    [Header("Values")]
    public Enemy enemyType;
    public MeshRenderer meshRenderer;
    [Space]
    [SerializeField]
    private float health;

    private Color originalColour;

    void Start()
    {
        if (!meshRenderer)
            meshRenderer = GetComponent<MeshRenderer>();
        originalColour = meshRenderer.material.color;
        health = enemyType.maxHealth;
    }

    IEnumerator HurtEffect(MeshRenderer toFlash, Color flashColor, float flashTime, float flashSpeed)
    {
        var flashingFor = 0f;
        var newColor = flashColor;
        while (flashingFor < flashTime)
        {
            toFlash.material.color = newColor;
            flashingFor += Time.deltaTime;
            yield return new WaitForSeconds(flashSpeed);
            flashingFor += flashSpeed;
            if (newColor == flashColor)
            {
                newColor = originalColour;
            }
            else
            {
                newColor = flashColor;
            }
        }
        toFlash.material.color = originalColour;
    }

    public void Hurt(float damage)
    {
        health -= damage;
        StartCoroutine(HurtEffect(meshRenderer, Color.red, 0.5f, 0.08f));
        if (health == 0)
            Destroy(gameObject);
    }
}
