using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ShopCharacter : MonoBehaviour
{
	public Player player => Tree.GameManager.player;

	[SerializeField] private Canvas _canves;
	[SerializeField] private TextWriter _textWriter;

	[SerializeField] private TextMeshProUGUI[] _talkText;
	[SerializeField] private RectTransform _buySell;

	private Animator _anim;

	private int _currentText;

	private void Start()
	{
		_anim = GetComponent<Animator>();
	}

	public void GoToNext()
	{
		if (_currentText < _talkText.Length - 1)
		{
			_currentText++;
			_textWriter.StartTypewriter(_talkText[_currentText]);
		}

		if (_currentText == _talkText.Length - 1 && _textWriter.IsEnded())
		{
			_buySell.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
		}
	}

	public void OpenTalk()
	{
		_currentText = 0;

		_anim.SetBool("Hi", true);

		_buySell.localScale = Vector2.zero;

		_canves.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject).OnComplete(() =>
		{
			_textWriter.StartTypewriter(_talkText[_currentText]);
		});
	}

	public void CloseTalk()
	{
		_anim.SetBool("Hi", false);

		_textWriter.StopTalk();

		_canves.transform.DOScale(0f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
		_buySell.DOScale(0f, 0.3f).SetEase(Ease.OutBack).SetId(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			OpenTalk();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			CloseTalk();
		}
	}
}
