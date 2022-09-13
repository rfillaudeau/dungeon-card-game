using System;
using UnityEngine;

public enum ResourceType
{
    Coin,
    Material,
}

public class ResourceCard : Card
{
    public static event Action<ResourceType, int> onAcquired;
    public event Action onCollected;

    [HideInInspector] public int quantity = 1;
    [HideInInspector] public ResourceType type = ResourceType.Coin;

    public void Acquire(Card acquirer)
    {
        onAcquired?.Invoke(type, quantity);
        onCollected?.Invoke();

        TranslateTowards(acquirer.transform);

        if (slot != null)
        {
            slot.card = null;
        }

        Destroy(gameObject, Card.AnimationsDelay);
    }
}
