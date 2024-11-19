namespace Domain.Utilities
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public static class Utils
    {
        private const int FLOOR_LAYER_MASK = 8;
        private const int RAY_DEPTH   = 100;
        private const int BUILDINGS_LAYERS = 6;
        
        public static Vector3 GetMouseClickHit(Camera mainCamera)
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
            
            int layerMask = 1 << FLOOR_LAYER_MASK;
            
            return Physics.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), 
                mainCamera.transform.forward*RAY_DEPTH, out RaycastHit hit, Mathf.Infinity, layerMask) 
                ? new Vector3(hit.point.x, hit.point.y, 0) : Vector3.zero;
        }

        public static Collider GetObjectAtPosition(Camera mainCamera)
        {
            if (mainCamera == null)
            {
                return null;
            }
            
            int layerMask = 1 << BUILDINGS_LAYERS;
            
            return Physics.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), 
                mainCamera.transform.forward*RAY_DEPTH, out RaycastHit hit, Mathf.Infinity, layerMask) 
                ? hit.collider : null;
        }

        public static Vector3 GetMouseWorldPosition()
        {
            var mainCamera = Camera.main;
            
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.y = 0f;
            return mouseWorldPosition;
        }

        public static Vector3 GetRandomDir()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1)).normalized;
        }
        
        public static bool CanSpawnAtPosition(BoxCollider boxCollider, Vector3 position)
        {
            int layerMask = 1 << 0;
            Collider[] hitColliders = new Collider[2];
            int numColliders = Physics.OverlapBoxNonAlloc(position, boxCollider.size/2, hitColliders, Quaternion.identity, layerMask);
            
            if (numColliders == 1 && hitColliders[0].gameObject == boxCollider.gameObject)
            {
                return true;
            }
            
            if (numColliders > 0)
            {
                return false;
            }

            return true;
        }

        public static bool IsPointerOverGameObject()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}