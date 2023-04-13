using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
	[SerializeField] ShopCharacter _shopCharacter;
	[SerializeField] TextMeshProUGUI tmpProText;
	[SerializeField] private Coroutine coroutine;

	[SerializeField] float delayBeforeStart = 0f;
	[SerializeField] float timeBtwChars = 0.1f;
	[SerializeField] string leadingChar = "";
	[SerializeField] bool leadingCharBeforeDelay = false;

	[Header("Collision-Based")]
	[SerializeField] private bool clearAtStart = false;
	
	private string _writer;

	private bool _isEnded;

	void Start()
	{
		if (!clearAtStart) return;

		if (tmpProText != null)
		{
			tmpProText.text = "";
		}
	}

	public void StartTypewriter(TextMeshProUGUI text)
	{
		_isEnded = false;

		_writer = text.text;

		StopAllCoroutines();

		if (tmpProText != null)
		{
			tmpProText.text = "";

			StartCoroutine(TypeWriterTMP());
		}
	}

	public void StopTalk()
	{
		StopAllCoroutines();
	}

	IEnumerator TypeWriterTMP()
	{
		tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in _writer)
		{
			if (tmpProText.text.Length > 0)
			{
				tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
			}
			tmpProText.text += c;
			tmpProText.text += leadingChar;
			yield return new WaitForSeconds(timeBtwChars);
		}

		if (leadingChar != "")
		{
			tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
		}

		yield return new WaitForSeconds(1f);

		_shopCharacter.GoToNext();
		_isEnded = true;
	}

	public bool IsEnded()
	{
		return _isEnded;
	}
}
