using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGamePanel : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private SettingPanel _settingPanel;
    [SerializeField] private ScoreManager _scoreManager;

    private GridGenerator _gridGenerator;
    private void Start()
    {
        _gridGenerator = FindObjectOfType<GridGenerator>();
    }
    public void StartSoloGame()
    {
        gameObject.SetActive(false);
        _settingPanel.Setup(GameModeEnum.SinglePlayer);
    }

    public void StartMultiplayerGame()
    {
        gameObject.SetActive(false);
        _gridGenerator.Initialize(GameModeEnum.Multiplayer);
        Player player1 = Instantiate(_playerPrefab, _gridGenerator.GetRandomCell().SpawnPosition.position, _playerPrefab.transform.rotation).GetComponent<Player>();
        Player player2 = Instantiate(_playerPrefab, _gridGenerator.GetRandomCell().SpawnPosition.position, _playerPrefab.transform.rotation).GetComponent<Player>();
        player1.Initialize(2);
        player2.Initialize(3);
        _scoreManager.Initialize(player1, player2);
        _scoreManager.gameObject.SetActive(true);
    }
    
    public void Exit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
