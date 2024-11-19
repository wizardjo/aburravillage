using UnityEngine;

namespace Game.Interfaces
{
    public interface IDraggable
    {
        public Transform GetTransform();
        public bool IsDraggingEnabled();
        public void UpdatePosition(Vector2 position);
    }
}
