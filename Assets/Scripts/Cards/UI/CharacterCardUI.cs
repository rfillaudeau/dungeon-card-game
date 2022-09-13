using UnityEngine;
using TMPro;

public class CharacterCardUI : MonoBehaviour
{
    [SerializeField] private CharacterCard _card;

    [SerializeField] private TextMeshPro _typeText;
    [SerializeField] private GameObject _playerIndicator;

    private void Start()
    {
        _typeText.SetText(_card.type.ToString());
        _playerIndicator.SetActive(_card.isPlayerCard);
    }
}
