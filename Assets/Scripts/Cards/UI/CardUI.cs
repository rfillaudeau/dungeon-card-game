using UnityEngine;
using TMPro;

public class CardUI : MonoBehaviour
{
    [SerializeField] private Card _card;

    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _nameText.SetText(_card.cardName);
        _spriteRenderer.sprite = _card.sprite;
    }
}
