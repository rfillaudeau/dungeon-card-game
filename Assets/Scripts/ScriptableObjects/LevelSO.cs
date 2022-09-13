using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class LevelSO : ScriptableObject
{
    public string levelName;
    public CardDropDataWithTurn[] monsterDrops;
    public CardDropDataWithTurn[] weaponDrops;
    public CardDropDataWithTurn[] potionDrops;
    public CardDropDataWithTurn[] resourceDrops;

    private CardDropDataWithTurn[] _allDrops;
    private bool _isAllDropsInitialized = false;

    public CardDropDataWithTurn[] GetAllDrops()
    {
        if (_isAllDropsInitialized)
        {
            return _allDrops;
        }

        int length = monsterDrops.Length + weaponDrops.Length + potionDrops.Length + resourceDrops.Length;
        _allDrops = new CardDropDataWithTurn[length];
        int index = 0;

        foreach (CardDropDataWithTurn drop in monsterDrops)
        {
            _allDrops[index] = drop;
            index++;
        }

        foreach (CardDropDataWithTurn drop in weaponDrops)
        {
            _allDrops[index] = drop;
            index++;
        }

        foreach (CardDropDataWithTurn drop in potionDrops)
        {
            _allDrops[index] = drop;
            index++;
        }

        foreach (CardDropDataWithTurn drop in resourceDrops)
        {
            _allDrops[index] = drop;
            index++;
        }

        _isAllDropsInitialized = true;

        return _allDrops;
    }
}
