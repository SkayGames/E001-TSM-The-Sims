using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LandBuy : MonoBehaviour
{
	public LandsManager landManager;

	[SerializeField] private RectTransform _landPage;

	[SerializeField] private Button _buyButton;

	[SerializeField] private TextMeshProUGUI _buyPrice;

	[SerializeField] private float _landPrice;

	private void OnMouseEnter()
	{
		if (landManager.TotalLandsBought < landManager.maxLandsToBuy)
			OpenLandPage(true);
	}

	private void OnMouseExit()
	{
		OpenLandPage(false);
	}

	private void OpenLandPage(bool state)
	{
		_buyPrice.text = "BUY " + _landPrice + "$";

		if (state) _landPage.DOScale(0.290172f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
		else _landPage.DOScale(0f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
	}

	public void BuyLand()
	{
		if(Tree.GameManager.Coins >= _landPrice)
		{
			Tree.GameManager.Coins -= _landPrice;

			if (landManager.TotalLandsBought < landManager.maxLandsToBuy)
			{
				landManager.BuyNewLand();
			}
		}
	}
}
