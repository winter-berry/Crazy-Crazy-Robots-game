using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    /* Customizable */
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Image fill;

    private void Update()
    {
        healthSlider.transform.position = Camera.main.WorldToScreenPoint(transform.root.position + offset);
    }

    public void SetActive(float health, float maxHealth)
    {
        gameObject.SetActive(health < maxHealth);
    }

    public void SetMaxHealth(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }
}
