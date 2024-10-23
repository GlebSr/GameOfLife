using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayerSettings : MonoBehaviour
{
    private GridGenerator _gridGenerator;

    [SerializeField] private Scrollbar _scrollbar;
    public void Initialize(GridGenerator gridGenerator)
    {
        gameObject.SetActive(true);
        _gridGenerator = gridGenerator;
    }

    public void ChangeSpeed()
    {
        _gridGenerator.GenerationGap = _scrollbar.value;
    }
    
    public void Home()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FillRandom()
    {
        _gridGenerator.FillRandomCells();
    }
}
