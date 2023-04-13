using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandsManager : MonoBehaviour
{
	public Land land;

	public int totalLandsBought;

	[SerializeField] private Transform _landParent;

	[SerializeField] private float _xOffSet;
	[SerializeField] private float _yOffSet;

	private List<Transform> m_lands = new List<Transform>();

	private int _xIndex;
	private int _yIndex;

	private void Start()
	{
		Init();
	}

	public void Init()
	{
		InitLands();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
			BuyNewLand();
	}

	private void InitLands()
	{
		_xIndex = 0;

		if (m_lands.Count > 0)
		{
			for (int i = 0; i < m_lands.Count; i++)
			{
				Destroy(m_lands[i].gameObject);
			}

			m_lands.Clear();
		}

		Vector3 m_instPosition = new Vector3(7.561f, -3.528f, -0.02f);

		int m_instaniatedLands = 0;

		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				if (m_instaniatedLands < TotalLandsBought)
				{
					Land m_land = Instantiate(land, m_instPosition, Quaternion.identity, _landParent);
					m_instaniatedLands++;
					m_land.transform.localPosition = m_instPosition;
					m_lands.Add(m_land.transform);

					//Set Index
					m_land.GetSavedData(m_lands.Count - 1);
					m_land.Init();

					_xIndex++;
				}
				else
					return;

				m_instPosition = new Vector3(m_lands[m_lands.Count - 1].localPosition.x + _xOffSet, m_instPosition.y, -0.02f);
			}

			_xIndex = 0;
			m_instPosition = new Vector3(7.561f, m_lands[m_lands.Count - 1].localPosition.y + _yOffSet, -0.02f);
		}
	}

	public void BuyNewLand()
	{
		if (_xIndex == 5)
			_xIndex = 0;

		Vector3 m_instPosition = new Vector3(7.561f, -3.528f, -0.02f);

		if (m_lands.Count > 0)
			m_instPosition = new Vector3(m_lands[m_lands.Count - 1].localPosition.x + _xOffSet, m_lands[m_lands.Count - 1].localPosition.y, -0.02f);

		if (_xIndex == 0 && m_lands.Count > 0)
			m_instPosition = new Vector3(7.561f, m_lands[m_lands.Count - 1].localPosition.y + _yOffSet, -0.02f);

		Land m_land = Instantiate(land, m_instPosition, Quaternion.identity, _landParent);
		m_land.transform.localPosition = m_instPosition;

		m_lands.Add(m_land.transform);

		//Set Index
		m_land.GetSavedData(m_lands.Count - 1);
		m_land.Init();

		if (_xIndex < 5)
			_xIndex++;

		TotalLandsBought++;
	}

	public int TotalLandsBought
	{
		get
		{
			totalLandsBought = PlayerPrefs.GetInt("TotalLandsBought", totalLandsBought);
			return totalLandsBought;
		}
		set
		{
			totalLandsBought = value;
			PlayerPrefs.SetInt("TotalLandsBought", totalLandsBought);
		}
	}
}
