using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

public class Land : MonoBehaviour
{
	public Plants plantType;

	[Header("UI")]
	[SerializeField] private RectTransform _collectPopUp;
	[SerializeField] private RectTransform _plantPopUp;
	[SerializeField] private TextMeshProUGUI _timerText;
	[SerializeField] private Button collectButton;

	[Space]
	[SerializeField] private float _secondsToGrow;

	[Space]
	[SerializeField] private SpriteRenderer _plantImage;
	[SerializeField] private SpriteRenderer _outline;

	private SpriteRenderer _mySprite;

	private float _msToWaitPlantsGrow;

	private ulong _lastTimeChecked;

	private string _lastTimeSave = "timeSaved";

	private int _showOrder = 10;
	private int _startOrder;

	private bool _isBeenPlanted;

	public void GetSavedData(int index)
	{
		_isBeenPlanted = PlayerPrefs.GetInt("ItsPlanted" + index) == 0 ? false : true;
		plantType = (Plants)PlayerPrefs.GetInt("PlantType" + index);
	}

	public void Init()
	{
		_mySprite = transform.GetComponent<SpriteRenderer>();
		_startOrder = _mySprite.sortingOrder;

		InitTimer();
	}

	private void OnMouseEnter()
	{
		_mySprite.sortingOrder = _showOrder;
		_outline.sortingOrder = _showOrder - 1;

		_outline.gameObject.SetActive(true);
		transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
	}

	private void OnMouseDown()
	{
		if (_isBeenPlanted)
			CollectPopUp(true);
		else
			PlantPopUp(true);
	}

	private void OnMouseExit()
	{
		_mySprite.sortingOrder = _startOrder;
		_outline.sortingOrder = _startOrder - 1;

		_outline.gameObject.SetActive(false);
		transform.DOScale(0.8f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);

		CollectPopUp(false);
		PlantPopUp(false);
	}

	private void CollectPopUp(bool open)
	{
		if (open) _collectPopUp.DOScale(0.002046806f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
		else _collectPopUp.DOScale(0f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
	}

	private void PlantPopUp(bool open)
	{
		if (open) _plantPopUp.DOScale(0.002046806f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
		else _plantPopUp.DOScale(0f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
	}

	private void CheckPlantState()
	{
		if (SecondsLeft() > 0 && _isBeenPlanted)
		{
			//Show middle state of plant
			_plantImage.sprite = GetItem(plantType).itemDesign;
		}

		if (SecondsLeft() == 0 && _isBeenPlanted)
		{
			//Show last state of plant
			_plantImage.sprite = GetItem(plantType).collectedItem.itemDesign;
		}
	}

	public Item GetItem(Plants planteType)
	{
		for (int i = 0; i < Tree.GameManager.plantsItems.Length; i++)
		{
			if (planteType == Tree.GameManager.plantsItems[i].plants)
				return Tree.GameManager.plantsItems[i];
		}

		return null;
	}

	#region LAND TIMER
	public void InitTimer()
	{
		_msToWaitPlantsGrow = _secondsToGrow * 1000;

		if (PlayerPrefs.GetString(_lastTimeSave) == "")
			ResetTime();

		_lastTimeChecked = ulong.Parse(PlayerPrefs.GetString(_lastTimeSave));
	}

	private void Update()
	{
		//Plant check state
		CheckPlantState();

		if (Availiable())
		{
			collectButton.interactable = true;
			return;
		}

		collectButton.interactable = false;

		ulong diff = ((ulong)DateTime.Now.Ticks - _lastTimeChecked);
		ulong ms = diff / TimeSpan.TicksPerMillisecond;
		float secondsLeft = (float)(_msToWaitPlantsGrow - ms) / 1000.0f;

		string t = "";

		if ((int)secondsLeft / 3600 != 0)
		{
			//HOURES
			t += ((int)secondsLeft / 3600).ToString() + "h:";
			secondsLeft -= ((int)secondsLeft / 3600) * 3600;
		}

		if ((int)secondsLeft / 60 != 0)
		{
			//MINUTES
			t += ((int)secondsLeft / 60).ToString("00") + "m:";
		}

		//SECONDS
		t += (secondsLeft % 60).ToString("00") + "s";

		_timerText.text = t;
	}

	public void GetPlant()
	{
		if (!Availiable())
			return;

		ResetTime();
	}

	private void ResetTime()
	{
		_lastTimeChecked = (ulong)DateTime.Now.Ticks;
		PlayerPrefs.SetString(_lastTimeSave, _lastTimeChecked.ToString());
	}

	private bool Availiable()
	{
		ulong diff = ((ulong)DateTime.Now.Ticks - _lastTimeChecked);
		ulong ms = diff / TimeSpan.TicksPerMillisecond;

		float secondsLeft = (float)(_msToWaitPlantsGrow - ms) / 1000.0f;

		if (secondsLeft < 0)
		{
			return true;
		}

		return false;
	}

	private float SecondsLeft()
	{
		ulong diff = ((ulong)DateTime.Now.Ticks - _lastTimeChecked);
		ulong ms = diff / TimeSpan.TicksPerMillisecond;

		float secondsLeft = (float)(_msToWaitPlantsGrow - ms) / 1000.0f;

		return secondsLeft;
	}

	#endregion
}
