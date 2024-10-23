using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private GridGenerator _gridGenerator;
    [SerializeField] private TMP_Text _player1Score;
    [SerializeField] private TMP_Text _player2Score;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private WinPanel _winPanel;
    private Player _player1;
    private Player _player2;
    private float _timeLeft = 30.0f;
    public void Initialize(Player player1, Player player2)
    {
        _player1 = player1; 
        _player2 = player2;
        _player1.OnScoreChanged += UpdatePlayer1Score;
        _player2.OnScoreChanged += UpdatePlayer2Score;
        _player1Score.color = Manager.CellMaterials[_player1.Color].color;
        _player2Score.color = Manager.CellMaterials[_player2.Color].color;
    }
    // Start is called before the first frame update
    void Start()
    {
        _gridGenerator.CellRevived += AddScore;
    }

    private void AddScore(int color)
    {
        Debug.Log(Time.time + " " + "Add Score");
        if (color == _player1.Color)
        {
            _player1.AddPoint();
        }
        else
        {
            _player2.AddPoint();
        }
    }

    void UpdatePlayer1Score()
    {
        _player1Score.text = _player1.Score.ToString();
    }

    void UpdatePlayer2Score()
    {
        _player2Score.text = _player2.Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _timeLeft -= Time.deltaTime;
        if (_timeLeft < 0.0f)
        {
            if (_player1.Score > _player2.Score)
            {
                _winPanel.Initialize(1, _player1.Score);
            } else if (_player2.Score > _player1.Score)
            {
                _winPanel.Initialize(2, _player2.Score);
            }
            gameObject.SetActive(false);
        }
        _timer.text = _timeLeft.ToString("0.0") + 's';
    }
}
