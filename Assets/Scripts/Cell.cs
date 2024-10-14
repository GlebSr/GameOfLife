using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public enum NextCellStateEnum : byte { NoChange, MakeDead, MakeAlive }
    public class Cell : MonoBehaviour
    {
        private const int NumberOfNeighbors = 8;
        private Renderer _renderer;
        private int _cellState;

        private Index[] _myNeighbors;
        private Index _me;
        
        public bool IsSumSet { get; set; }

        private int _sum;
        private int Sum
        {
            get
            {
                if (IsSumSet)
                {
                    return _sum;
                }

                Debug.LogErrorFormat("Trying to get a not set sum. Index: {0}", _me);
                Manager.GameState = GameStateEnum.Invalid;
                return -1;
            }
            set
            {
                IsSumSet = true;
                _sum = value;
            }
        }

        public NextCellStateEnum NextCellState { get; set; }
        public int NextAliveCellColor { get; set; }
        public bool ChangeColor { get; set; }
        private bool _isAlive;
        public bool IsAlive
        {
            get { return _isAlive; }
            set
            {
                _isAlive = value;
                _cellState = _isAlive ? NextAliveCellColor : 0;
                NextAliveCellColor = 1;
                _renderer.sharedMaterial = Manager.CellMaterials[_cellState];
            }
        }

        public void Initialize(int h, int w, int gridWidth, int gridHeight)
        {
            NextAliveCellColor = 1;
            var gridWidthMinusOne = gridWidth - 1;
            var gridHeightMinusOne = gridHeight - 1;

            var wPlusOne = w + 1;
            var wMinusOne = w - 1;
            var hPlusOne = h + 1;
            var hMinusOne = h - 1;

            _renderer = GetComponent<Renderer>();
            NextAliveCellColor = Random.Range(0, Manager.GameMode == GameModeEnum.Multiplayer ? 4 : 2);
            IsAlive = NextAliveCellColor != 0;
            NextCellState = NextCellStateEnum.NoChange;
            _myNeighbors = new Index[NumberOfNeighbors];
            _me = new Index { W = w, H = h};

            _myNeighbors[0] = new Index
            {
                H = h,
                W = wMinusOne < 0 ? gridWidthMinusOne : wMinusOne,
            };

            _myNeighbors[1] = new Index
            {
                H = hPlusOne > gridHeightMinusOne ? 0 : hPlusOne,
                W = wMinusOne < 0 ? gridWidthMinusOne : wMinusOne,
            };

            _myNeighbors[2] = new Index
            {
                H = hPlusOne > gridHeightMinusOne ? 0 : hPlusOne,
                W = w,
            };

            _myNeighbors[3] = new Index
            {
                H = hPlusOne > gridHeightMinusOne ? 0 : hPlusOne,
                W = wPlusOne > gridWidthMinusOne ? 0 : wPlusOne,
            };

            _myNeighbors[4] = new Index
            {
                H = h,
                W = wPlusOne > gridWidthMinusOne ? 0 : wPlusOne,
            };

            _myNeighbors[5] = new Index
            {
                H = hMinusOne < 0 ? gridHeightMinusOne : hMinusOne,
                W = wPlusOne > gridWidthMinusOne ? 0 : wPlusOne,
            };

            _myNeighbors[6] = new Index
            {
                H = hMinusOne < 0 ? gridHeightMinusOne : hMinusOne,
                W = w,
            };

            _myNeighbors[7] = new Index
            {
                H = hMinusOne < 0 ? gridHeightMinusOne : hMinusOne,
                W = wMinusOne < 0 ? gridWidthMinusOne : wMinusOne,
            };

        }

        public int CalculateCellSum(Cell[,] cells)
        {
            if (IsSumSet) return Sum;
            if(true)
            {
                NextAliveCellColor = 
                    _myNeighbors.Select(neighbor => cells[neighbor.H, neighbor.W]._cellState)
                        .Concat(new[] { _cellState })
                        .Where(state => state != 0)
                        .GroupBy(state => state)
                        .OrderByDescending(group => group.Count())
                        .FirstOrDefault()?.Key ?? 1;
                ChangeColor = NextAliveCellColor != _cellState;
            }
            Sum = (IsAlive ? 1 : 0) + _myNeighbors.Sum(neighbor => cells[neighbor.H, neighbor.W].IsAlive ? 1 : 0);
            return Sum;
        }

        private void OnMouseDown()
        {
            if (Manager.GameState == GameStateEnum.AcceptInput)
            {
                NextAliveCellColor = Random.Range(1, 4);
                IsAlive = !IsAlive;
            }
        }

        
    }

    internal struct Index
    {
        internal int W;
        internal int H;
        public override string ToString()
        {
            return "Cell (" + H + "," + W + ")";
        }
    }
}
