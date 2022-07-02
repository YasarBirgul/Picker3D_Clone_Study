using UnityEngine;

namespace Controllers
{
    public class LevelLoaderCommand : MonoBehaviour
    {
        public void InitializeLevel(int _levelID,GameObject levelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level{_levelID}"),levelHolder.transform);
        }
    }
}