using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
	public ShopManager shopManager;
	public GamePlayUi gamePlay;

	[SerializeField] private RectTransform _tapToPlay;

	public void Init()
	{
		TapToPlay(true);
	}

	public void ChangeCoinsText(float coins)
	{
		gamePlay.ChangeTextUi(coins);
	}

	public void TapToPlay(bool state)
	{
		if (state)
			_tapToPlay.gameObject.SetActive(true);

		if (state) _tapToPlay.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
		else _tapToPlay.DOScale(0f, 0.3f).SetEase(Ease.OutBack).OnComplete(() => _tapToPlay.gameObject.SetActive(false));
	}
}
