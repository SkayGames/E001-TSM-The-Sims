using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
	public static CameraManager instance;

    public CinemachineVirtualCamera cinemachineVirtualCamera;

	private float _shakeTimer;

	private CinemachineBasicMultiChannelPerlin cinemachinePerlin;

	private void Awake()
	{
		instance = this;
	}

	public void ShakeCamera(float intensity, float time)
	{
		CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		
		cinemachinePerlin = cinemachineBasicMultiChannelPerlin;
		cinemachinePerlin.m_AmplitudeGain = intensity;

		_shakeTimer = time;
	}

	private void Update()
	{
		if (_shakeTimer > 0)
		{
			_shakeTimer -= Time.deltaTime;

			if(_shakeTimer <= 0f)
			{
				cinemachinePerlin.m_AmplitudeGain = 0f;
			}
		}
	}
}
