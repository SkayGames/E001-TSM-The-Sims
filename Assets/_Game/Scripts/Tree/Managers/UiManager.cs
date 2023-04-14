using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
	public ShopManager shopManager;
	public GamePlayUi gamePlay;

	public void Init()
	{

	}

	public void ChangeCoinsText(float coins)
	{
		gamePlay.ChangeTextUi(coins);
	}
}
