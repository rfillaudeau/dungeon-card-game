using UnityEngine;

public abstract class CardSO : ScriptableObject
{
    public string cardName => _cardName;
    public Sprite sprite => _sprite;

    [SerializeField] private string _cardName;
    [SerializeField] private Sprite _sprite;
}
