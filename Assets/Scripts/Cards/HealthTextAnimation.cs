using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HealthTextAnimation : MonoBehaviour
{
    [SerializeField] private Damageable _damageable;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _damageable.onDamaged += Damaged;
        _damageable.onHealed += Healed;
    }

    private void OnDisable()
    {
        _damageable.onDamaged -= Damaged;
        _damageable.onHealed -= Healed;
    }

    private void Damaged(int damage)
    {
        _animator.SetTrigger("Damaged");
    }

    private void Healed(int heal)
    {
        _animator.SetTrigger("Healed");
    }
}
