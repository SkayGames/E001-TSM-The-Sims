using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlantItemUi : MonoBehaviour
{
	[SerializeField] private Image _itemDesign;

	[SerializeField] private TextMeshProUGUI _sellPrice;
	[SerializeField] private TextMeshProUGUI _quanitity;

	[SerializeField] private Button _sellButton;

	public void Init(Sprite design, float sellPrice, int quanitity, Action action)
	{
		_sellButton.onClick.RemoveAllListeners();

		_sellButton.onClick.AddListener(() => action());

		_itemDesign.sprite = design;

		_sellPrice.text = "SELL" + sellPrice;
		_quanitity.text = "" + quanitity;
	}
}
