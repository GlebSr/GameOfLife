using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGamePanel : MonoBehaviour
{
    private GridGenerator _gridGenerator;

    private void Start()
    {
        _gridGenerator = FindObjectOfType<GridGenerator>();
    }

    public void StartSoloGame()
    {
        _gridGenerator.Initialize(GameModeEnum.SinglePlayer);
        gameObject.SetActive(false);
    }

    public void StartMultiplayerGame()
    {
        _gridGenerator.Initialize(GameModeEnum.Multiplayer);
        gameObject.SetActive(false);
    }
    
    public void Exit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
