using UnityEngine;
using TMPro;

public class DamageableUI : MonoBehaviour
{
    [SerializeField] private Damageable _damageable;

    [SerializeField] private TextMeshPro _healthText;

    private void OnEnable()
    {
        _damageable.onHealthUpdated += UpdateHealthText;
    }

    private void OnDisable()
    {
        _damageable.onHealthUpdated -= UpdateHealthText;
    }

    private void UpdateHealthText(int health)
    {
        _healthText.SetText(health.ToString());
    }
}
