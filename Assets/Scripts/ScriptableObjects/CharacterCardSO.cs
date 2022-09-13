using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Card", menuName = "Cards/Character")]
public class CharacterCardSO : CardSO
{
    public int maxHealth;
    public CharacterType type;
    public WeaponCardSO weapon;
    public CardDropData[] drops;
}

[Serializable]
public class CardDropData
{
    public CardSO card;

    [Range(0f, 100f)]
    public float dropPercentage;
}

[Serializable]
public class CardDropDataWithTurn : CardDropData
{
    public int dropAtTurn;
}
