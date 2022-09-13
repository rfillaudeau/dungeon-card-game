using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCardUpgrade
{
    public CharacterCardSO characterCard;
    public int maxHealthUpgrade = 0;
}

public class GameManager : MonoBehaviour
{
    public static event Action<int> onCoinsUpdated;

    public static GameManager instance { get; private set; }

    public int coins { get; private set; }
    public LevelSO selectedLevel { get; private set; }
    public CharacterCardSO selectedCharacter { get; private set; }
    public List<CharacterCardUpgrade> charactersUpgrades;

    [SerializeField] private LevelSO _defaultLevel;
    [SerializeField] private CharacterCardSO _defaultCharacter;

    public void SelectLevel(LevelSO level)
    {
        selectedLevel = level;
    }

    public void SelectCharacter(CharacterCardSO character)
    {
        selectedCharacter = character;
    }

    public void AddResource(ResourceType type, int quantity)
    {
        if (type == ResourceType.Coin)
        {
            coins += quantity;

            onCoinsUpdated?.Invoke(coins);

            return;
        }
    }

    public void RemoveResource(ResourceType type, int quantity)
    {
        if (type == ResourceType.Coin)
        {
            coins -= quantity;

            onCoinsUpdated?.Invoke(coins);

            return;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

            return;
        }

        instance = this;

        charactersUpgrades = new List<CharacterCardUpgrade>();

        selectedLevel = _defaultLevel;
        selectedCharacter = _defaultCharacter;

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        ResourceCard.onAcquired += AddResource;
        WeaponCard.onConvertedToResource += AddResource;
    }

    private void OnDisable()
    {
        ResourceCard.onAcquired -= AddResource;
        WeaponCard.onConvertedToResource -= AddResource;
    }
}
