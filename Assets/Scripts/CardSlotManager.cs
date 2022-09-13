using System.Collections;
using UnityEngine;

public class CardSlotManager : MonoBehaviour
{
    public static System.Action onNewTurn;
    public static System.Action onGameOver;

    [SerializeField] private CardBuilder _cardBuilder;

    [SerializeField] private CardSlot[] _slots;
    [SerializeField] private int _rowLength = 3;
    [SerializeField] private int _maxMonsters = 3;
    [SerializeField] private int _maxWeapons = 2;

    [SerializeField] private int turn = 1;

    private CharacterCard _playerCard;

    private LevelSO _level;
    private CharacterCardSO _character;

    private bool _canSelect = false;

    private int _weaponCount = 0;
    private int _monsterCount = 0;

    private void Start()
    {
        int x = 0;
        int y = _slots.Length / _rowLength - 1;
        foreach (CardSlot slot in _slots)
        {
            if (x >= _rowLength)
            {
                x = 0;
                y--;
            }

            slot.gridPosition = new Vector2Int(x, y);

            x++;
        }

        _level = GameManager.instance.selectedLevel;
        _character = GameManager.instance.selectedCharacter;

        StartCoroutine(StartLevel());
    }

    private void OnEnable()
    {
        CardSlot.onSelected += CardSlotSelected;
    }

    private void OnDisable()
    {
        CardSlot.onSelected -= CardSlotSelected;
    }

    private void CardSlotSelected(CardSlot slot)
    {
        if (!_canSelect || !CanInteractWithSlot(slot))
        {
            return;
        }

        _canSelect = false;

        CardSlot previousSlot = _playerCard.slot;

        InteractWithCard(slot.card);

        if (_playerCard.damageable.health == 0)
        {
            onGameOver?.Invoke();

            return;
        }

        StartCoroutine(MoveCards(slot, previousSlot));

        turn++;

        onNewTurn?.Invoke();
    }

    private IEnumerator MoveCards(CardSlot currentSlot, CardSlot previousSlot)
    {
        // Wait for interaction animation
        yield return new WaitForSeconds(Card.AnimationsDelay);

        if (currentSlot.card == null)
        {
            _playerCard.MoveToSlot(currentSlot);
        }

        if (previousSlot.card == null)
        {
            // Move cards to fill empty slot and create a new card
            Vector2Int direction = _playerCard.slot.gridPosition - previousSlot.gridPosition;

            MoveCardsToEmptySlot(previousSlot, direction);
        }

        if (currentSlot.card == null || previousSlot.card == null)
        {
            // Wait for cards to move
            yield return new WaitForSeconds(Card.AnimationsDelay);
        }

        StartCoroutine(FillEmptySlots());
    }

    private Vector2Int GetNewDirection(Vector2Int direction)
    {
        if (direction == Vector2Int.down)
        {
            return Vector2Int.left;
        }
        else if (direction == Vector2Int.up)
        {
            return Vector2Int.right;
        }
        else if (direction == Vector2Int.right)
        {
            return Vector2Int.down;
        }
        else
        {
            return Vector2Int.up;
        }
    }

    private void CountCards()
    {
        _monsterCount = 0;
        _weaponCount = 0;

        foreach (CardSlot slot in _slots)
        {
            if (slot.card == null)
            {
                continue;
            }

            if (slot.card is CharacterCard && ((CharacterCard)slot.card).type == CharacterType.Monster)
            {
                _monsterCount++;
            }

            if (slot.card is WeaponCard)
            {
                _weaponCount++;
            }
        }
    }

    private IEnumerator FillEmptySlots()
    {
        CountCards();

        bool areCardsMoving = true;
        while (areCardsMoving)
        {
            areCardsMoving = false;
            foreach (CardSlot slot in _slots)
            {
                if (slot.card == null)
                {
                    continue;
                }

                if (slot.card.isMoving)
                {
                    areCardsMoving = true;
                }
            }

            yield return null;
        }

        foreach (CardSlot slot in _slots)
        {
            if (slot.card != null)
            {
                continue;
            }

            if (_monsterCount < _maxMonsters)
            {
                foreach (CardDropDataWithTurn drop in _level.monsterDrops)
                {
                    if (turn >= drop.dropAtTurn && Random.Range(0f, 100f) <= drop.dropPercentage)
                    {
                        _cardBuilder.BuildCard(drop.card).SpawnInSlot(slot);
                        _monsterCount++;
                        break;
                    }
                }
            }

            if (slot.card != null)
            {
                continue;
            }

            if (_weaponCount < _maxWeapons)
            {
                foreach (CardDropDataWithTurn drop in _level.weaponDrops)
                {
                    if (turn >= drop.dropAtTurn && Random.Range(0f, 100f) <= drop.dropPercentage)
                    {
                        _cardBuilder.BuildCard(drop.card).SpawnInSlot(slot);
                        _weaponCount++;
                        break;
                    }
                }
            }

            if (slot.card != null)
            {
                continue;
            }

            foreach (CardDropDataWithTurn drop in _level.potionDrops)
            {
                if (turn >= drop.dropAtTurn && Random.Range(0f, 100f) <= drop.dropPercentage)
                {
                    _cardBuilder.BuildCard(drop.card).SpawnInSlot(slot);
                    break;
                }
            }

            if (slot.card != null)
            {
                continue;
            }

            foreach (CardDropDataWithTurn drop in _level.resourceDrops)
            {
                if (turn >= drop.dropAtTurn && Random.Range(0f, 100f) <= drop.dropPercentage)
                {
                    _cardBuilder.BuildCard(drop.card).SpawnInSlot(slot);
                    break;
                }
            }
        }

        _canSelect = true;
    }

    private IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(Card.AnimationsDelay);

        _playerCard = _cardBuilder.BuildCharacterCard(_character);
        _playerCard.isPlayerCard = true;

        foreach (CharacterCardUpgrade upgrade in GameManager.instance.charactersUpgrades)
        {
            if (upgrade.characterCard != _character)
            {
                continue;
            }

            _playerCard.damageable.UpgradeMaxHealth(upgrade.maxHealthUpgrade);
        }

        _playerCard.SpawnInSlot(GetCardSlotAtPosition(new Vector2Int(1, 1)));

        yield return new WaitForSeconds(Card.AnimationsDelay);

        StartCoroutine(FillEmptySlots());
    }

    private void MoveCardsToEmptySlot(CardSlot emptySlot, Vector2Int direction)
    {
        bool hasMovedLine = false;
        while (true)
        {
            Vector2Int positionToMove = emptySlot.gridPosition - direction;
            CardSlot slotToMove = GetCardSlotAtPosition(positionToMove);
            if (slotToMove == null || slotToMove.card == null || slotToMove.card == _playerCard)
            {
                break;
            }

            slotToMove.card.MoveToSlot(emptySlot);
            emptySlot = slotToMove;

            hasMovedLine = true;
        }

        if (!hasMovedLine)
        {
            Vector2Int newDirection = direction;

            while (!hasMovedLine)
            {
                newDirection = GetNewDirection(newDirection);
                if (newDirection == direction)
                {
                    break;
                }

                while (true)
                {
                    Vector2Int positionToMove = emptySlot.gridPosition - newDirection;
                    CardSlot slotToMove = GetCardSlotAtPosition(positionToMove);
                    if (slotToMove == null || slotToMove.card == null || slotToMove.card == _playerCard)
                    {
                        break;
                    }

                    slotToMove.card.MoveToSlot(emptySlot);
                    emptySlot = slotToMove;

                    hasMovedLine = true;
                }
            }
        }
    }

    private bool CanInteractWithSlot(CardSlot slot)
    {
        Vector2Int a = _playerCard.slot.gridPosition;
        Vector2Int b = slot.gridPosition;

        int distance = Mathf.Abs(b.x - a.x) + Mathf.Abs(b.y - a.y);

        return distance == 1;
    }

    private void InteractWithCard(Card card)
    {
        if (card == null)
        {
            return;
        }

        if (card is CharacterCard)
        {
            _playerCard.Attack((CharacterCard)card);

            return;
        }

        if (card is WeaponCard)
        {
            _playerCard.equipment.EquipWeapon((WeaponCard)card);

            return;
        }

        if (card is ResourceCard)
        {
            ((ResourceCard)card).Acquire(_playerCard);

            return;
        }

        if (card is PotionCard)
        {
            ((PotionCard)card).HealCharacter(_playerCard);

            return;
        }
    }

    private CardSlot GetCardSlotAtPosition(Vector2Int gridPosition)
    {
        foreach (CardSlot slot in _slots)
        {
            if (slot.gridPosition == gridPosition)
            {
                return slot;
            }
        }

        return null;
    }
}
