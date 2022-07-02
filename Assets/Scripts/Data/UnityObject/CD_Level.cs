using System.Collections.Generic;

namespace Data.ValueObject
{
    [UnityEngine.CreateAssetMenu(fileName = "CD_Level", menuName = "Picker3D/CD_Level", order = 0)]
    public class CD_Level : UnityEngine.ScriptableObject
    {
        public List<LevelData> Levels = new List<LevelData>();
    }
}