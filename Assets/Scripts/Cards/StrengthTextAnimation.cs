using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StrengthTextAnimation : MonoBehaviour
{
    [SerializeField] private Equipment _equipment;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _equipment.onWeaponEquipped += WeaponEquipped;
        _equipment.onWeaponUnequipped += UnsubscribeToWeapon;

        SubscribeToWeapon();
    }

    private void OnDisable()
    {
        _equipment.onWeaponEquipped -= WeaponEquipped;
        _equipment.onWeaponUnequipped -= UnsubscribeToWeapon;

        UnsubscribeToWeapon();
    }

    private void Damaged(int value)
    {
        _animator.SetTrigger("Damaged");
    }

    private void WeaponEquipped(WeaponCard weapon)
    {
        SubscribeToWeapon();
    }

    private void SubscribeToWeapon()
    {
        if (_equipment.weapon == null)
        {
            return;
        }

        _equipment.weapon.onStrengthDecreased += Damaged;
    }

    private void UnsubscribeToWeapon()
    {
        if (_equipment.weapon == null)
        {
            return;
        }

        _equipment.weapon.onStrengthDecreased -= Damaged;
    }
}
