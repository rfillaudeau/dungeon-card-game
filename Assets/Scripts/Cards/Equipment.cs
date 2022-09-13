using System;
using System.Collections;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public event Action<WeaponCard> onWeaponEquipped;
    public event Action onWeaponUnequipped;

    public WeaponCard weapon { get; private set; }

    public void EquipWeapon(WeaponCard newWeapon, bool withDelay = true)
    {
        if (weapon != null)
        {
            weapon.ConvertToResource();

            UnequipWeapon();
        }

        weapon = newWeapon;
        weapon.SetEquipped();
        weapon.onStrengthDisappeared += UnequipWeapon;

        if (weapon.slot != null)
        {
            weapon.slot.card = null;
        }

        StartCoroutine(EquipWeaponCoroutine(withDelay));
    }

    public void UnequipWeapon()
    {
        if (weapon != null)
        {
            weapon.onStrengthDisappeared -= UnequipWeapon;
            Destroy(weapon.gameObject);
        }

        weapon = null;

        onWeaponUnequipped?.Invoke();
    }

    private void OnEnable()
    {
        if (weapon != null)
        {
            weapon.onStrengthDisappeared += UnequipWeapon;
        }
    }

    private void OnDisable()
    {
        if (weapon != null)
        {
            weapon.onStrengthDisappeared -= UnequipWeapon;
        }
    }

    private IEnumerator EquipWeaponCoroutine(bool withDelay)
    {
        if (withDelay)
        {
            weapon.TranslateTowards(transform);

            yield return new WaitForSeconds(Card.AnimationsDelay);
        }

        weapon.transform.SetParent(transform);

        weapon.transform.localScale = Vector3.zero;
        weapon.transform.position = Vector3.zero;
        weapon.transform.localPosition = Vector3.zero;

        onWeaponEquipped?.Invoke(weapon);
    }
}
