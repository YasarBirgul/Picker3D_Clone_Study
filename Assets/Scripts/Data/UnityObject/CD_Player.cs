using UnityEngine;

namespace Data.ValueObject
{
    [CreateAssetMenu(fileName = "CD_PLayer", menuName = "Picker3D/CD_Player", order = 0)]
    public class CD_Player : ScriptableObject
    {
        public PlayerData PlayerData;
    }
}