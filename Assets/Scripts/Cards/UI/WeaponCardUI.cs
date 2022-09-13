using UnityEngine;
using TMPro;

public class WeaponCardUI : MonoBehaviour
{
    [SerializeField] private WeaponCard _weapon;

    [SerializeField] private TextMeshPro _strengthText;

    private void Start()
    {
        UpdateStrengthText(_weapon.strength);
    }

    private void OnEnable()
    {
        _weapon.onStrengthUpdated += UpdateStrengthText;
    }

    private void OnDisable()
    {
        _weapon.onStrengthUpdated += UpdateStrengthText;
    }

    private void UpdateStrengthText(int strength)
    {
        _strengthText.SetText(strength.ToString());
    }
}
