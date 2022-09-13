using UnityEngine;
using TMPro;

public class ResourceCardUI : MonoBehaviour
{
    [SerializeField] private ResourceCard _resource;

    [SerializeField] private TextMeshPro _quantityText;

    private void Start()
    {
        _quantityText.SetText(_resource.quantity.ToString());
    }
}
