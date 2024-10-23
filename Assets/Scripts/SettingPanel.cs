using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;


public class SettingPanel : MonoBehaviour
{
    [SerializeField] private SinglePlayerSettings _singlePlayerSettings;
    private GridGenerator _gridGenerator;
    GameModeEnum _gameMode;
    private float _cameraScale = 0.55f;

    [SerializeField]
    private TMP_InputField _heightInput;
    
    [SerializeField]
    private TMP_InputField _widthInput;
    private void Start()
    {
        _gridGenerator = FindObjectOfType<GridGenerator>();
    }

    public void Setup(GameModeEnum mode)
    {
        _gameMode = mode;
        gameObject.SetActive(true);
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        _gridGenerator.Height = int.Parse(_heightInput.text);
        _gridGenerator.Width = int.Parse(_widthInput.text);
        FindObjectOfType<CameraController>().SetSize(Math.Max(_gridGenerator.Height, _gridGenerator.Width) * _cameraScale);
        _gridGenerator.Initialize(_gameMode);
        _singlePlayerSettings.Initialize(_gridGenerator);
    }
}
