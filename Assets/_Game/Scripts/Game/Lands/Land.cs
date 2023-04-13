using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Land : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _outline;

	private int _showOrder = 10;
	private int _startOrder;

	private SpriteRenderer _mySprite;

	private void OnEnable()
	{
		_mySprite = transform.GetComponent<SpriteRenderer>();
		_startOrder = _mySprite.sortingOrder;
	}

	private void OnMouseEnter()
	{
		_mySprite.sortingOrder = _showOrder;
		_outline.sortingOrder = _showOrder - 1;

		_outline.gameObject.SetActive(true);
		transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
	}

	private void OnMouseExit()
	{
		_mySprite.sortingOrder = _startOrder;
		_outline.sortingOrder = _startOrder - 1;

		_outline.gameObject.SetActive(false);
		transform.DOScale(0.8f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
	}
}
