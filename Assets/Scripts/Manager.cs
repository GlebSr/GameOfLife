using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts
{
    public enum GameStateEnum : byte { Invalid, Wait, AcceptInput, Run }
    public enum GameModeEnum : byte { SinglePlayer, Multiplayer}
    public static class Manager
    {    
        public static GameStateEnum GameState;
        public static GameModeEnum GameMode;
        
        public static GameObject CellPrefab;
        public static Material[] CellMaterials;
        public static Material[] PlayerMaterials;

        public static bool Initialize()
        {
            var result = true;

            CellPrefab = Resources.Load<GameObject>("Prefabs/Cell");
            Assert.IsNotNull(CellPrefab, "Cell prefab not found");
            if (CellPrefab == null)
            {
                GameState = GameStateEnum.Invalid;
                Debug.LogError("Cell prefab not found");
                result = false;
            }

            CellMaterials = new[]
            {
                Resources.Load<Material>("Materials/Dead"),
                Resources.Load<Material>("Materials/Alive"),
                Resources.Load<Material>("Materials/FirstPlayerCell"),
                Resources.Load<Material>("Materials/SecondPlayerCell")
            };
            PlayerMaterials = new[]
            {
                Resources.Load<Material>("Materials/Players/FirstPlayer"),
                Resources.Load<Material>("Materials/Players/SecondPlayer"),
            };

            for (var i = 0; i < CellMaterials.Length; i++)
            {
                if (CellMaterials[i] != null) continue;
                GameState = GameStateEnum.Invalid;
                Debug.LogErrorFormat("Cell Material {0} not found", i);
                result = false;
                break;
            }

            return result;
        }
    }
}
