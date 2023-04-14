using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GamePlayUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coins;

    public void ChangeTextUi(float coins)
	{
		_coins.text = $"{coins.ToString("0.00")}$";
	}
}
