using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
	[SerializeField] private PlantItemUi _plantItem;
	[SerializeField] private RectTransform _plantItemParent;
	[SerializeField] private RectTransform _envintoryEmpty;

	[SerializeField] private RectTransform _shop;
	[SerializeField] private RectTransform _clothesSection;
	[SerializeField] private RectTransform _inventorySection;

	[SerializeField] private Button _shopButton;
	[SerializeField] private Button _inventoryButton;

	public ClothesItemUi[] heads;
	public ClothesItemUi[] torsos;

	private PlantItemUi _plantsItemsList;

	private void Start()
	{
		for (int i = 0; i < heads.Length; i++)
		{
			heads[i].Init();
		}

		for (int i = 0; i < torsos.Length; i++)
		{
			torsos[i].Init();
		}
	}

	public void OpenShop(bool state)
	{
		InitInventory();

		if (state)
			_shop.gameObject.SetActive(true);

		if (state) _shop.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
		else _shop.DOScale(0f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject).OnComplete(() => _shop.gameObject.SetActive(false));

		OpenClothes(true);
	}

	public void OpenClothes(bool state)
	{
		if (state)
			_clothesSection.gameObject.SetActive(true);

		if (state) _clothesSection.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
		else _clothesSection.DOScale(0f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject).OnComplete(() => _clothesSection.gameObject.SetActive(false));

		_inventoryButton.interactable = state;
		_shopButton.interactable = !state;

		if (state)
			OpenInventory(false);
	}

	public void OpenInventory(bool state)
	{
		if (state)
			_inventorySection.gameObject.SetActive(true);

		if (state) _inventorySection.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
		else _inventorySection.DOScale(0f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject).OnComplete(() => _inventorySection.gameObject.SetActive(false));

		_inventoryButton.interactable = !state;
		_shopButton.interactable = state;

		if (state)
			OpenClothes(false);
	}

	public void UnequipeHeads(ClothesHead headType)
	{
		for (int i = 0; i < heads.Length; i++)
		{
			if (headType == heads[i].clothesHead)
			{
				continue;
			}

			heads[i].Unequipe();
			heads[i].Init();
		}
	}

	public void UnequipeTorso(ClothesTorso torsoType)
	{
		for (int i = 0; i < torsos.Length; i++)
		{
			if (torsoType == torsos[i].clothesTorso)
			{
				continue;
			}

			torsos[i].Unequipe();
			torsos[i].Init();
		}
	}

	public void InitInventory()
	{
		if (_plantsItemsList != null)
			Destroy(_plantsItemsList.gameObject);

		if (Tree.GameManager.BoughtPlants > 0)
		{
			_envintoryEmpty.gameObject.SetActive(false);

			Item m_itemPlant = Tree.GameManager.plantsItems[0];

			PlantItemUi m_plantItem = Instantiate(_plantItem, Vector2.zero, Quaternion.identity, _plantItemParent);
			_plantsItemsList = m_plantItem;

			m_plantItem.Init(m_itemPlant.collectedItem.itemDesign, m_itemPlant.collectedItem.itemSellPrice, Tree.GameManager.BoughtPlants, SellItemPlant);
		}
		else if (Tree.GameManager.BoughtPlants == 0)
		{
			_envintoryEmpty.gameObject.SetActive(true);

			if (_plantsItemsList != null)
				Destroy(_plantsItemsList.gameObject);
		}
	}

	public void SellItemPlant()
	{
		Item m_itemPlant = Tree.GameManager.plantsItems[0];

		Tree.GameManager.BoughtPlants--;

		_plantsItemsList.Init(m_itemPlant.collectedItem.itemDesign, m_itemPlant.collectedItem.itemSellPrice, Tree.GameManager.BoughtPlants, SellItemPlant);

		//Give him money
		Tree.GameManager.Coins += m_itemPlant.collectedItem.itemSellPrice;

		if (Tree.GameManager.BoughtPlants == 0)
		{
			_envintoryEmpty.gameObject.SetActive(true);

			if (_plantsItemsList != null)
				Destroy(_plantsItemsList.gameObject);
		}
	}
}
