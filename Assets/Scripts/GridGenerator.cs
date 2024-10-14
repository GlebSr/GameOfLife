using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts
{
    public class GridGenerator : MonoBehaviour
    {
        [Range(3, 150)]
        [Tooltip("Grid Width (Number of Columns)")]
        public int Width;

        [Range(3, 150)]
        [Tooltip("Grid Height (Number of Rows)")]
        public int Height;

        [Range(0.01f, 1.0f)]
        [Tooltip("Delay in seconds between display of two generations")]
        public float GenerationGap;

        private static readonly Vector3 CellScale = Vector3.one * 0.8f;

        private Cell[,] _cells;
        private int _width, _height;
        private bool _hasRunCoroutineFinished;

        public void Initialize(GameModeEnum gameMode)
        {
            Manager.GameMode = gameMode;
            _width = Width;
            _height = Height;
            _hasRunCoroutineFinished = true;

            Assert.IsTrue(_width > 2, "Width should be greater than 2 for proper simulation to occur");
            Assert.IsTrue(_height > 2, "Height should be greater than 2 for proper simulation to occur");

            if (_width < 3 ||  _height < 3)
            {
                Manager.GameState = GameStateEnum.Invalid;
                Debug.LogError("Invalid width or height or depth");
                return;
            }

            if (Manager.Initialize())
            {
                Manager.GameState = GameStateEnum.Wait;
            }
            _cells = new Cell[_height, _width];
            PopulateGrid();
        }
        
        /*private void Awake()
        {
            _width = Width;
            _height = Height;
            _hasRunCoroutineFinished = true;

            Assert.IsTrue(_width > 2, "Width should be greater than 2 for proper simulation to occur");
            Assert.IsTrue(_height > 2, "Height should be greater than 2 for proper simulation to occur");

            if (_width < 3 || _height < 3)
            {
                Manager.GameState = GameStateEnum.Invalid;
                Debug.LogError("Invalid width or height or depth");
                return;
            }

            if (Manager.Initialize())
            {
                Manager.GameState = GameStateEnum.Wait;
            }
        }*/

        /*private void Start()
        {
            if (Manager.GameState == GameStateEnum.Invalid) return;
            _cells = new Cell[_height, _width];
            PopulateGrid();
        }*/

        private void PopulateGrid()
        {
            var offset = new Vector2Int
            {
                x = _width - Mathf.FloorToInt(0.5f * (_width - 1) + 1.0f),
                y = _height - Mathf.FloorToInt(0.5f * (_height - 1) + 1.0f),
            };


                for (var h = 0; h < _height; h++)
                {
                    for (var w = 0; w < _width; w++)
                    {

                        _cells[h, w] = Instantiate(Manager.CellPrefab, transform).GetComponent<Cell>();

#if UNITY_EDITOR
                        _cells[h, w].gameObject.name = "Cell (" + h + "," + w + ")";
#endif

                        var cellTransform = _cells[h, w].transform;
                        cellTransform.position = new Vector3(w - offset.x, h - offset.y);
                        cellTransform.rotation = Quaternion.identity;
                        cellTransform.localScale = CellScale;

                        _cells[h, w].Initialize(h, w, _width, _height);
                    }
                }
            

            Manager.GameState = GameStateEnum.AcceptInput;
        }

        private void UpdateCells()
        {
            
                for (var h = 0; h < _height; h++)
                {
                    for (var w = 0; w < _width; w++)
                    {
                        var sum = _cells[h, w].CalculateCellSum(_cells);
                        
                            switch (sum)
                            {
                                case 3:
                                    _cells[h, w].NextCellState = _cells[h, w].IsAlive
                                        ? NextCellStateEnum.NoChange
                                        : NextCellStateEnum.MakeAlive;
                                    break;
                                case 4:
                                    _cells[h, w].NextCellState = NextCellStateEnum.NoChange;
                                    break;
                                default:
                                    _cells[h, w].NextCellState = _cells[h, w].IsAlive
                                        ? NextCellStateEnum.MakeDead
                                        : NextCellStateEnum.NoChange;
                                    break;
                            }
                        
                    }
                }
            
        }

        private void ApplyCellUpdates()
        {
            
                for (var h = 0; h < _height; h++)
                {
                    for (var w = 0; w < _width; w++)
                    {
                        if (_cells[h, w].NextCellState == NextCellStateEnum.MakeDead)
                        {
                            _cells[h, w].IsAlive = false;
                        }
                        else if (_cells[h, w].NextCellState == NextCellStateEnum.MakeAlive)
                        {
                            _cells[h, w].IsAlive = true;
                        } else if (_cells[h, w].NextCellState == NextCellStateEnum.NoChange && _cells[h, w].IsAlive &&
                                   _cells[h, w].ChangeColor)
                        {
                            _cells[h, w].IsAlive = true;
                        }

                        _cells[h, w].IsSumSet = false;
                    }
                }
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (Manager.GameState == GameStateEnum.AcceptInput && _hasRunCoroutineFinished)
                {
                    Manager.GameState = GameStateEnum.Run;
                    _hasRunCoroutineFinished = false;
                    StartCoroutine(Run());           
                }
                else if (Manager.GameState == GameStateEnum.Run)
                {
                    Manager.GameState = GameStateEnum.AcceptInput;
                }
            }
        }
        private IEnumerator Run()
        {
            while (Manager.GameState == GameStateEnum.Run)
            {
                UpdateCells();
                ApplyCellUpdates();
                yield return new WaitForSeconds(GenerationGap);
            }
            _hasRunCoroutineFinished = true;
        }
    }
}
