using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _winnerText;

    public void Initialize(int winner, int score)
    {
        gameObject.SetActive(true);
        Manager.GameState = GameStateEnum.Wait;
        _winnerText.text = "Player" + winner + " wins!";
    }

    // Update is called once per frame
    public void Home()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
