using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour
{
    public static event Action<CardSlot> onSelected;

    public Card card;
    public Vector2Int gridPosition;

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        onSelected?.Invoke(this);
    }
}
