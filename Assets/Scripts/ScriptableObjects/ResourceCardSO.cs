using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Card", menuName = "Cards/Resource")]
public class ResourceCardSO : CardSO
{
    public int quantity;
    public ResourceType type;
}
