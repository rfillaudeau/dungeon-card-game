using UnityEngine;
using TMPro;

public class PotionCardUI : MonoBehaviour
{
    [SerializeField] private PotionCard _card;

    [SerializeField] private TextMeshPro _healText;

    private void Start()
    {
        _healText.SetText(_card.heal.ToString());
    }
}
