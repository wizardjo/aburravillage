using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BuildingData", fileName = "new BuildingData")]
    public class BuildingData : ScriptableObject
    {
        public int id;
        public bool isDraggable;
    }
}