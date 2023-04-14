using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class ClothesItemUi : MonoBehaviour
{
	public ClothesType clothesType;

	[ShowIf("clothesType", ClothesType.Head)]
	public ClothesHead clothesHead;

	[ShowIf("clothesType", ClothesType.Torso)]
	public ClothesTorso clothesTorso;

	[SerializeField] private RectTransform _buyButton;
	[SerializeField] private Button _equipeButton;

	[SerializeField] private TextMeshProUGUI _equipeText;
	[SerializeField] private TextMeshProUGUI _priceToUnlock;

	[SerializeField] private Image _itemDesign;

	private bool _isUnlocked;
	private bool _isEquiped;

	public void Init()
	{
		_itemDesign.sprite = GetClothesItem().itemDesign;
		_priceToUnlock.text = "BUY " + GetClothesItem().buyPrice;

		if (clothesType == ClothesType.Head)
		{
			_isEquiped = PlayerPrefs.GetInt("IsEquiped" + clothesHead) == 0 ? false : true;
			_isUnlocked = PlayerPrefs.GetInt("BuyClothes" + clothesHead) == 0 ? false : true;
		}
		else
		{
			_isEquiped = PlayerPrefs.GetInt("IsEquiped" + clothesTorso) == 0 ? false : true;
			_isUnlocked = PlayerPrefs.GetInt("BuyClothes" + clothesTorso) == 0 ? false : true;
		}

		_buyButton.gameObject.SetActive(!_isUnlocked);

		if (_isEquiped)
		{
			_equipeText.text = "Equiped";
			_equipeButton.interactable = false;
		}
		else
		{
			_equipeText.text = "Equipe";
			_equipeButton.interactable = true;
		}
	}

	public void BuyClothes()
	{
		if (GetClothesItem().buyPrice <= Tree.GameManager.Coins)
		{
			Tree.GameManager.Coins -= GetClothesItem().buyPrice;

			if (clothesType == ClothesType.Head)
			{
				PlayerPrefs.SetInt("BuyClothes" + clothesHead, 1);
			}
			else
			{
				PlayerPrefs.SetInt("BuyClothes" + clothesTorso, 1);
			}

			_isUnlocked = true;

			Init();
		}
	}

	public void EquipeItem()
	{

		if (clothesType == ClothesType.Head)
		{
			PlayerPrefs.SetInt("IsEquiped" + clothesHead, 1);

			Tree.UIManager.shopManager.UnequipeHeads(clothesHead);
			Tree.GameManager.UpdatePlayerHead(GetClothesItem(), (int)GetClothesItem().clothesHead);
		}
		else
		{
			PlayerPrefs.SetInt("IsEquiped" + clothesTorso, 1);

			Tree.UIManager.shopManager.UnequipeTorso(clothesTorso);
			Tree.GameManager.UpdatePlayerToros(GetClothesItem(), (int)GetClothesItem().clothesTorso);
		}

		_isEquiped = true;

		Init();
	}

	public void Unequipe()
	{
		if (clothesType == ClothesType.Head)
		{
			PlayerPrefs.SetInt("IsEquiped" + clothesHead, 0);
		}
		else
		{
			PlayerPrefs.SetInt("IsEquiped" + clothesTorso, 0);
		}

		_isEquiped = false;
	}

	public ClothesItems GetClothesItem()
	{
		ClothesItems[] m_items = clothesType == ClothesType.Head ? Tree.GameManager.clothesHead : Tree.GameManager.clothesTorso;

		for (int i = 0; i < m_items.Length; i++)
		{
			if (clothesType == ClothesType.Head)
			{
				if (m_items[i].clothesHead == clothesHead)
				{
					return m_items[i];
				}
			}
			else
			{
				if (m_items[i].clothesTorso == clothesTorso)
				{
					return m_items[i];
				}
			}
		}

		return null;
	}
}
