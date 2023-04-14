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
	[SerializeField] private ParticleSystem _takePlantFX;
	[SerializeField] private Transform _takePlantFXPosition;

	[Header("UI")]
	[SerializeField] private Image _fillImage;
	[SerializeField] private RectTransform _collectPopUp;
	[SerializeField] private RectTransform _plantPopUp;
	[SerializeField] private TextMeshProUGUI _timerText;

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
	private bool _isHolding;
	private bool _isMouseUp;

	private int index;

	public void GetSavedData(int m_index)
	{
		index = m_index;

		_isBeenPlanted = PlayerPrefs.GetInt("ItsPlanted" + m_index) == 0 ? false : true;
		plantType = (Plants)PlayerPrefs.GetInt("PlantType" + m_index);
	}

	public void Init()
	{
		transform.DOScale(0.8f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);

		_mySprite = transform.GetComponent<SpriteRenderer>();
		_startOrder = _mySprite.sortingOrder;

		if (_isBeenPlanted)
			InitTimer(false);
	}

	private void OnMouseEnter()
	{
		_mySprite.sortingOrder = _showOrder;
		_outline.sortingOrder = _showOrder - 1;

		_outline.gameObject.SetActive(true);
		transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);

		if (_isBeenPlanted)
			CollectPopUp(true);
		else
			PlantPopUp(true);
	}

	private void OnMouseDown()
	{
		_isHolding = true;
	}

	private void OnMouseExit()
	{
		_isHolding = _isMouseUp = false;

		_mySprite.sortingOrder = _startOrder;
		_outline.sortingOrder = _startOrder - 1;

		_outline.gameObject.SetActive(false);
		transform.DOScale(0.8f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);

		_fillImage.fillAmount = 0f;

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

		if (SecondsLeft() <= 0 && _isBeenPlanted)
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

	public void GetPlant()
	{
		CameraManager.instance.ShakeCamera(0.5f, 0.1f);

		PlantFx();

		_isBeenPlanted = false;

		plantType = Plants.None;

		PlayerPrefs.SetInt("ItsPlanted" + index, 0);
		PlayerPrefs.SetInt("PlantType" + index, (int)Plants.None);

		_plantImage.sprite = null;

		Tree.GameManager.BoughtPlants++;

		OnMouseExit();
		//Add to inventory plant collected
	}

	public void PlantNewSeed()
	{
		CameraManager.instance.ShakeCamera(0.5f, 0.1f);

		//Buy seeds
		PlantFx();

		_isBeenPlanted = true;

		plantType = Plants.OrangePlant;

		PlayerPrefs.SetInt("ItsPlanted" + index, 1);
		PlayerPrefs.SetInt("PlantType" + index, (int)Plants.OrangePlant);

		OnMouseExit();

		InitTimer(true);
	}

	public void PlantFx()
	{
		ParticleSystem m_fx = Instantiate(_takePlantFX, _takePlantFXPosition.position, Quaternion.identity);
	}

	#region LAND TIMER
	public void InitTimer(bool newPlant)
	{
		_msToWaitPlantsGrow = _secondsToGrow * 1000;

		if (newPlant)
			ResetTime();

		_lastTimeChecked = ulong.Parse(PlayerPrefs.GetString(_lastTimeSave));
	}

	private void Update()
	{
		if (_isHolding)
		{
			if (Input.GetMouseButton(0))
			{
				if (_isBeenPlanted && !Availiable())
					return;

				if (_fillImage.fillAmount < 1f)
					_fillImage.fillAmount += Time.deltaTime;

				if (_fillImage.fillAmount >= 1f)
				{
					if (_isBeenPlanted && Availiable())
						GetPlant();
					else
						PlantNewSeed();
				}
			}

			if (Input.GetMouseButtonUp(0))
			{
				_isMouseUp = true;
			}

			if (_isMouseUp)
			{
				if (_fillImage.fillAmount > 0f)
					_fillImage.fillAmount -= Time.deltaTime;
			}
		}

		//Plant check state
		CheckPlantState();

		if (Availiable())
		{
			//collectButton.interactable = true;
			return;
		}

		//collectButton.interactable = false;

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
