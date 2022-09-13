using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinsText : MonoBehaviour
{
    private TextMeshProUGUI _coinsText;

    private void Awake()
    {
        _coinsText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateCoinsText(GameManager.instance.coins);
    }

    private void OnEnable()
    {
        GameManager.onCoinsUpdated += UpdateCoinsText;
    }

    private void OnDisable()
    {
        GameManager.onCoinsUpdated -= UpdateCoinsText;
    }

    private void UpdateCoinsText(int coins)
    {
        _coinsText.SetText($"Coins: {coins}");
    }
}
