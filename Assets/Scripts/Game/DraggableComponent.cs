using Game.Interfaces;
using UnityEngine;

namespace Game
{
    public class DraggableComponent : MonoBehaviour, IDraggable
    {
        [SerializeField]
        private BuildingData buildingData;

        public Transform GetTransform()
        {
            return transform;
        }

        public bool IsDraggingEnabled()
        {
            return buildingData.isDraggable;
        }

        public void UpdatePosition(Vector2 position)
        {
            transform.position = position;
        }

        public void Interact()
        {
            Debug.Log(buildingData.name);
        }
    }
}
