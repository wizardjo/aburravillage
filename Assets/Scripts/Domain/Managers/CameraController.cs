
using Cinemachine;
using DG.Tweening;
using Domain.Utilities;
using Game.Interfaces;
using UnityEngine;

namespace Domain.Managers
{
    public class CameraController : MonoBehaviour
    {
        private CinemachineVirtualCamera cinemachineVirtualCamera;
        private Vector3 touchStart;
        private IDraggable selectedObject;
        private bool touchStarted;
        private bool isDragging;
        public Vector2 minBounds;
        public Vector2 maxBounds;

        private void Awake()
        {
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                selectedObject = null;
                var selectedObjectTransform = Utils.GetBuildCollider();

                if (selectedObjectTransform != null && selectedObjectTransform.transform.GetComponent<IDraggable>() != null 
                                                    && selectedObjectTransform.GetComponent<IDraggable>().IsDraggingEnabled())
                {
                    selectedObject = selectedObjectTransform.GetComponent<IDraggable>();
                    var selectedObjectPosition = selectedObjectTransform.transform.position;
                    var destination = new Vector3(selectedObjectPosition.x,
                        selectedObjectPosition.y, transform.position.z);
                    transform.DOMove(destination, 0.35f).SetEase(Ease.OutQuad).OnComplete(
                        () =>
                        {
                            selectedObject.Interact();
                        });
                    return;
                }
                touchStart = Utils.GetMouseClickAtFloor();
                touchStarted = touchStart != Vector3.zero;
            }
            
            if (Input.GetMouseButton(0) && touchStarted)
            {
                var direction = touchStart - Utils.GetMouseClickAtFloor();

                if (selectedObject == null)
                {
                    //Just move the camera
                    transform.position += direction;
                }
                else
                {
                    selectedObject.UpdatePosition(new Vector3(Mathf.RoundToInt(Utils.GetMouseClickAtFloor().x), Mathf.RoundToInt(Utils.GetMouseClickAtFloor().y)));
                }
                
                
                isDragging = true;
            }
            else
            {
                if (isDragging)
                {
                    isDragging = false;
                    cinemachineVirtualCamera.m_Follow = null;
                }

                if (touchStarted)
                {
                    touchStarted = false;
                }
            }
        }
    }
}
