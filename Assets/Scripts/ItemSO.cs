using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Create Item")]
public class ItemSO : ScriptableObject
{
    [Header("Scroe Value")]

    public int point = 10;
}
