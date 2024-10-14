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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
