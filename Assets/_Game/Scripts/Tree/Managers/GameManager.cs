using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool gameStarted => _gameStarted;

	public ClothesItems[] clothesHead;
	public ClothesItems[] clothesTorso;

	public Item[] plantsItems;
	public Player player;

	private float _coins;

	private bool _gameStarted;

	private int _playerHeadIndex;
	private int _playerTorosIndex;
	private int _totalBoughtPlants;

	public void Init()
	{
		Coins = Coins;

		if (!IsFirstTimeHead())
		{
			for (int i = 0; i < clothesHead.Length; i++)
			{
				if ((int)clothesHead[i].clothesHead == SavedHead)
				{
					UpdatePlayerHead(clothesHead[i], SavedHead);
				}
			}
		}

		if (!IsFirstTimeToros())
		{
			for (int i = 0; i < clothesTorso.Length; i++)
			{
				if ((int)clothesTorso[i].clothesTorso == SavedToros)
				{
					UpdatePlayerToros(clothesTorso[i], SavedToros);
				}
			}
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && !_gameStarted)
			StartGame();
	}

	public void StartGame()
	{
		_gameStarted = true;

		Tree.UIManager.TapToPlay(false);
	}

	public void UpdatePlayerHead(ClothesItems clothesItem, int clothesIndex)
	{
		SavedHead = clothesIndex;
		player.ChangeHeadDesign(clothesItem.itemDesign);
	}

	public void UpdatePlayerToros(ClothesItems clothesItem, int clothesIndex)
	{
		SavedToros = clothesIndex;
		player.ChangeTorosDesign(clothesItem.itemDesign);
	}

	public int BoughtPlants
	{
		get
		{
			_totalBoughtPlants = PlayerPrefs.GetInt("BoughtPlants", _totalBoughtPlants);
			return _totalBoughtPlants;
		}
		set
		{
			_totalBoughtPlants = value;
			PlayerPrefs.SetInt("BoughtPlants", _totalBoughtPlants);
		}
	}

	public int SavedHead
	{
		get
		{
			_playerHeadIndex = PlayerPrefs.GetInt("PlayerHeadIndex", _playerHeadIndex);
			return _playerHeadIndex;
		}
		set
		{
			_playerHeadIndex = value;
			PlayerPrefs.SetInt("PlayerHeadIndex", _playerHeadIndex);
		}
	}

	public int SavedToros
	{
		get
		{
			_playerTorosIndex = PlayerPrefs.GetInt("PlayerTorosIndex", _playerTorosIndex);
			return _playerTorosIndex;
		}
		set
		{
			_playerTorosIndex = value;
			PlayerPrefs.SetInt("PlayerTorosIndex", _playerTorosIndex);
		}
	}

	public float Coins
	{
		get
		{
			_coins = PlayerPrefs.GetFloat("Coins", _coins);
			return _coins;
		}
		set
		{
			_coins = value;
			PlayerPrefs.SetFloat("Coins", _coins);

			Tree.UIManager.ChangeCoinsText(_coins);
		}
	}

	public bool IsFirstTimeHead()
	{
		if (PlayerPrefs.GetInt("IsFirstTimeHead") == 0)
			return true;
		else
			return false;
	}

	public bool IsFirstTimeToros()
	{
		if (PlayerPrefs.GetInt("IsFirstTimeToros") == 0)
			return true;
		else
			return false;
	}
}
