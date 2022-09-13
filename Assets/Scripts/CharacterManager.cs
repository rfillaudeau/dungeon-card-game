using System;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private CharacterCardSO[] _characters;
    [SerializeField] private CardSlot[] _slots;
    [SerializeField] private CardBuilder _builder;
    [SerializeField] private GameObject _cardSelector;
    [SerializeField] private int _upgradeCoinCost = 50;

    private int _selectedIndex = 0;

    public void UpgradeSelectedCharacter()
    {
        if (GameManager.instance.coins < _upgradeCoinCost)
        {
            return;
        }

        CharacterCardUpgrade upgrade = GameManager.instance.charactersUpgrades[_selectedIndex];

        if (upgrade == null)
        {
            upgrade = new CharacterCardUpgrade();
            upgrade.characterCard = _characters[_selectedIndex];
            upgrade.maxHealthUpgrade = 0;
        }

        upgrade.maxHealthUpgrade++;

        ((CharacterCard)_slots[_selectedIndex].card).damageable.UpgradeMaxHealth(1);

        GameManager.instance.RemoveResource(ResourceType.Coin, _upgradeCoinCost);
    }

    private void OnEnable()
    {
        CardSlot.onSelected += SlotSelected;
    }

    private void OnDisable()
    {
        CardSlot.onSelected -= SlotSelected;
    }

    private void Start()
    {
        if (GameManager.instance.selectedCharacter == null)
        {
            GameManager.instance.SelectCharacter(_characters[0]);
        }

        if (GameManager.instance.charactersUpgrades.Count == 0)
        {
            for (int i = 0; i < _characters.Length; i++)
            {
                CharacterCardUpgrade upgrade = new CharacterCardUpgrade();
                upgrade.characterCard = _characters[i];
                upgrade.maxHealthUpgrade = 0;

                GameManager.instance.charactersUpgrades.Add(upgrade);
            }
        }

        for (int i = 0; i < _characters.Length; i++)
        {
            _slots[i].card = _builder.BuildCharacterCard(_characters[i]);
            _slots[i].card.transform.position = _slots[i].transform.position;
            _slots[i].card.transform.SetParent(transform);
        }

        _selectedIndex = Array.IndexOf(_characters, GameManager.instance.selectedCharacter);

        SetCardSelectorPosition(_slots[_selectedIndex].transform.position);

        for (int i = 0; i < _characters.Length; i++)
        {
            ((CharacterCard)_slots[i].card).damageable.UpgradeMaxHealth(
                GameManager.instance.charactersUpgrades[i].maxHealthUpgrade
            );
        }
    }

    private void SlotSelected(CardSlot slot)
    {
        _selectedIndex = Array.IndexOf(_slots, slot);

        GameManager.instance.SelectCharacter(_characters[_selectedIndex]);

        SetCardSelectorPosition(slot.transform.position);
    }

    private void SetCardSelectorPosition(Vector3 position)
    {
        _cardSelector.transform.position = new Vector3(
            position.x,
            position.y,
            _cardSelector.transform.position.z
        );
    }
}
