using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CardAnimation : MonoBehaviour
{
    [SerializeField] private Card _card;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _card.onSpawn += Spawned;

        if (_card is CharacterCard)
        {
            ((CharacterCard)_card).damageable.onDie += Destroyed;
            ((CharacterCard)_card).onAttackWithDirection += Attacked;
        }

        if (_card is ResourceCard)
        {
            ((ResourceCard)_card).onCollected += Acquired;
        }

        if (_card is WeaponCard)
        {
            ((WeaponCard)_card).onEquipped += Acquired;
        }

        if (_card is PotionCard)
        {
            ((PotionCard)_card).onCollected += Acquired;
        }
    }

    private void OnDisable()
    {
        _card.onSpawn -= Spawned;

        if (_card is CharacterCard)
        {
            ((CharacterCard)_card).damageable.onDie -= Destroyed;
            ((CharacterCard)_card).onAttackWithDirection -= Attacked;
        }

        if (_card is ResourceCard)
        {
            ((ResourceCard)_card).onCollected -= Acquired;
        }

        if (_card is WeaponCard)
        {
            ((WeaponCard)_card).onEquipped -= Acquired;
        }

        if (_card is PotionCard)
        {
            ((PotionCard)_card).onCollected -= Acquired;
        }
    }

    private void Spawned()
    {
        _animator.SetTrigger("Spawn");
    }

    private void Destroyed()
    {
        _animator.SetTrigger("Destroy");
    }

    private void Acquired()
    {
        _animator.SetTrigger("Acquire");
    }

    private void Attacked(Vector2Int direction)
    {
        if (direction == Vector2Int.right)
        {
            _animator.SetTrigger("AttackRight");
        }
        else if (direction == Vector2Int.left)
        {
            _animator.SetTrigger("AttackLeft");
        }
        else if (direction == Vector2Int.up)
        {
            _animator.SetTrigger("AttackUp");
        }
        else if (direction == Vector2Int.down)
        {
            _animator.SetTrigger("AttackDown");
        }
    }
}
