using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour
{
    public static Action<CardSlot> onSelected;

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
