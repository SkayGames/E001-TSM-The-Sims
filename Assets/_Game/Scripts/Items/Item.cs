using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum PlantType { Seed, Plant }
public enum Plants { None, OrangePlant }

[CreateAssetMenu(fileName = "Item", menuName = "Items/New item")]
public class Item : ScriptableObject
{
	[Title("Plant Type")]
	public PlantType plantType;

	[Title("Plants")]
	public Plants plants;

	[Title("Plant Design")]
	public Sprite itemDesign;

	[ShowIf("plantType", PlantType.Seed)]
	[Title("Plant Collected When Grow")]
	public Item collectedItem;

	[ShowIf("plantType", PlantType.Seed)]
	[Title("Plant Buy Price")]
	public float itemBuyPrice;

	[Title("Plant Sell Price")]
	public float itemSellPrice;

	[Title("Saving Name")]
	public string saveName;
}
