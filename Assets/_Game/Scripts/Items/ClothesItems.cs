using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum ClothesType { Head, Torso }
public enum ClothesHead { Head1, Head2, Head3, Head4 }
public enum ClothesTorso { Torso1, Torso2, Torso3, Torso4 }

[CreateAssetMenu(fileName = "Item", menuName = "Items/New clothes")]
public class ClothesItems : ScriptableObject
{
    public ClothesType clothesType;

    [ShowIf("clothesType", ClothesType.Head)]
    public ClothesHead clothesHead;

    [ShowIf("clothesType", ClothesType.Torso)]
    public ClothesTorso clothesTorso;

    [Title("Item design")]
    public Sprite itemDesign;

    [Title("Item buy price")]
    public float buyPrice;
}
