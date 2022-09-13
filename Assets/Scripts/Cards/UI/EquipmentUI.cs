using UnityEngine;
using TMPro;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField] private Equipment _equipment;

    [SerializeField] private TextMeshPro _strengthText;
    [SerializeField] private SpriteRenderer _weaponRenderer;

    private void Start()
    {
        if (_equipment.weapon == null)
        {
            WeaponUnequipped();
        }
        else
        {
            WeaponEquipped(_equipment.weapon);
        }
    }

    private void OnEnable()
    {
        _equipment.onWeaponEquipped += WeaponEquipped;
        _equipment.onWeaponUnequipped += WeaponUnequipped;

        SubscribeToWeapon();
    }

    private void OnDisable()
    {
        _equipment.onWeaponEquipped -= WeaponEquipped;
        _equipment.onWeaponUnequipped -= WeaponUnequipped;

        UnsubscribeToWeapon();
    }

    private void UpdateStrengthText(int attack)
    {
        _strengthText.SetText(attack.ToString());
    }

    private void WeaponEquipped(WeaponCard weapon)
    {
        _weaponRenderer.gameObject.SetActive(true);
        _weaponRenderer.sprite = weapon.sprite;

        SubscribeToWeapon();

        UpdateStrengthText(_equipment.weapon.strength);
    }

    private void WeaponUnequipped()
    {
        UnsubscribeToWeapon();

        UpdateStrengthText(0);

        _weaponRenderer.gameObject.SetActive(false);
    }

    private void SubscribeToWeapon()
    {
        if (_equipment.weapon == null)
        {
            return;
        }

        _equipment.weapon.onStrengthUpdated += UpdateStrengthText;
        _equipment.weapon.onStrengthDisappeared += WeaponUnequipped;
    }

    private void UnsubscribeToWeapon()
    {
        if (_equipment.weapon == null)
        {
            return;
        }

        _equipment.weapon.onStrengthUpdated -= UpdateStrengthText;
        _equipment.weapon.onStrengthDisappeared -= WeaponUnequipped;
    }
}
