using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class CellTrigger : MonoBehaviour
{
    [SerializeField] private Cell cell;

    public void ChangeColor(int color)
    {
        cell.NextAliveCellColor = color;
        cell.IsAlive = true;
    }
    private void OnMouseDown()
    {
        if (Manager.GameState == GameStateEnum.AcceptInput)
        {
            cell.NextAliveCellColor = 1;
            cell.IsAlive = !cell.IsAlive;
        }
    }
}
