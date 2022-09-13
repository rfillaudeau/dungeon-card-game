using System;
using UnityEngine;

public class WeaponCard : Card
{
    public static event Action<ResourceType, int> onConvertedToResource;
    public event Action onEquipped;

    public event Action<int> onStrengthUpdated;
    public event Action<int> onStrengthDecreased;
    public event Action onStrengthDisappeared;

    [HideInInspector] public int strength = 1;

    public void DecreaseStrength(int value)
    {
        strength -= value;

        if (strength < 0)
        {
            strength = 0;
        }

        onStrengthUpdated?.Invoke(strength);
        onStrengthDecreased?.Invoke(value);

        if (strength == 0)
        {
            onStrengthDisappeared?.Invoke();
        }
    }

    public void SetEquipped()
    {
        onEquipped?.Invoke();
    }

    public void ConvertToResource()
    {
        onConvertedToResource?.Invoke(ResourceType.Coin, strength);
    }
}
