using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Damageable), typeof(Equipment))]
public class CharacterCard : Card
{
    public event Action<Vector2Int> onAttackWithDirection;

    public Damageable damageable { get; private set; }
    public Equipment equipment { get; private set; }
    [HideInInspector] public CharacterType type = CharacterType.Hero;
    public Card drop { get; private set; }
    public bool isPlayerCard = false;

    public void SetDrop(Card drop)
    {
        this.drop = drop;
        this.drop.gameObject.SetActive(false);
    }

    public void Attack(CharacterCard card)
    {
        onAttackWithDirection?.Invoke(card.slot.gridPosition - slot.gridPosition);

        if (equipment.weapon != null)
        {
            int defenderHealth = card.damageable.health;
            card.damageable.TakeDamage(equipment.weapon.strength);
            equipment.weapon.DecreaseStrength(defenderHealth);

            return;
        }

        if (card.damageable.health > 0)
        {
            int defenderHealth = card.damageable.health;
            card.damageable.TakeDamage(damageable.health);
            damageable.TakeDamage(defenderHealth);
        }
    }

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
        equipment = GetComponent<Equipment>();
    }

    private void OnEnable()
    {
        damageable.onDie += Died;
    }

    private void OnDisable()
    {
        damageable.onDie -= Died;
    }

    private void Died()
    {
        if (drop == null)
        {
            slot.card = null;
        }
        else
        {
            drop.SetSlot(slot);
        }

        StartCoroutine(DiedCoroutine());
    }

    private IEnumerator DiedCoroutine()
    {
        yield return new WaitForSeconds(Card.AnimationsDelay);

        if (drop != null)
        {
            drop.gameObject.SetActive(true);
            drop.SpawnInSlot(slot);
        }

        Destroy(gameObject);
    }
}
