using System;
using UnityEngine;

public class PotionCard : Card
{
    public event Action onCollected;

    [HideInInspector] public int heal = 1;

    public void HealCharacter(CharacterCard card)
    {
        card.damageable.Heal(heal);

        onCollected?.Invoke();

        TranslateTowards(card.transform);

        if (slot != null)
        {
            slot.card = null;
        }

        Destroy(gameObject, Card.AnimationsDelay);
    }
}
