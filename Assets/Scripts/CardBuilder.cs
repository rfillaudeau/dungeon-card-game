using UnityEngine;

public class CardBuilder : MonoBehaviour
{
    [SerializeField] private CharacterCard _characterCardPrefab;
    [SerializeField] private WeaponCard _weaponCardPrefab;
    [SerializeField] private ResourceCard _resourceCardPrefab;
    [SerializeField] private PotionCard _potionCardPrefab;

    public Card BuildCard(CardSO data)
    {
        if (data is WeaponCardSO)
        {
            return BuildWeaponCard((WeaponCardSO)data);
        }

        if (data is CharacterCardSO)
        {
            return BuildCharacterCard((CharacterCardSO)data);
        }

        if (data is ResourceCardSO)
        {
            return BuildResourceCard((ResourceCardSO)data);
        }

        if (data is PotionCardSO)
        {
            return BuildPotionCard((PotionCardSO)data);
        }

        return null;
    }

    public CharacterCard BuildCharacterCard(CharacterCardSO data)
    {
        CharacterCard card = Instantiate(_characterCardPrefab);

        card.cardName = data.cardName;
        card.sprite = data.sprite;
        card.type = data.type;

        card.damageable.SetMaxHealth(data.maxHealth);

        if (data.weapon != null)
        {
            WeaponCard weaponCard = BuildWeaponCard(data.weapon);

            card.equipment.EquipWeapon(weaponCard, false);
        }

        if (data.drops.Length > 0)
        {
            foreach (CardDropData drop in data.drops)
            {
                if (Random.Range(0f, 100f) <= drop.dropPercentage)
                {
                    card.SetDrop(BuildCard(drop.card));
                }
            }
        }

        return card;
    }

    public WeaponCard BuildWeaponCard(WeaponCardSO data)
    {
        WeaponCard card = Instantiate(_weaponCardPrefab);

        card.cardName = data.cardName;
        card.sprite = data.sprite;
        card.strength = data.strength;

        return card;
    }

    public ResourceCard BuildResourceCard(ResourceCardSO data)
    {
        ResourceCard card = Instantiate(_resourceCardPrefab);

        card.cardName = data.cardName;
        card.sprite = data.sprite;
        card.quantity = data.quantity;
        card.type = data.type;

        return card;
    }

    public PotionCard BuildPotionCard(PotionCardSO data)
    {
        PotionCard card = Instantiate(_potionCardPrefab);

        card.cardName = data.cardName;
        card.sprite = data.sprite;
        card.heal = data.heal;

        return card;
    }
}
