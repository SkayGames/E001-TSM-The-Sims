using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private static Tree _instance;
    private static bool _initDone = false;

    public static GameManager GameManager => _instance._gameManager;
    public static UiManager UIManager => _instance._uIManager;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UiManager _uIManager;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _gameManager.Init();
        _uIManager.Init();
        _initDone = true;
    }
}
