using System;
using Cinemachine;
using Game.Interfaces;
using UnityEngine;

namespace Domain.Managers
{
    using Domain.Utilities;

    public class CameraController : MonoBehaviour
    {
        private CinemachineVirtualCamera cinemachineVirtualCamera;
        private Vector3 touchStart;
        private IDraggable selectedObject;
        private bool touchStarted;
        private bool isDragging;

        private void Awake()
        {
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !Utils.IsPointerOverGameObject())
            {
                touchStart = Utils.GetMouseClickHit(Camera.main);
                selectedObject = null;
                var selectedObjectTransform = Utils.GetObjectAtPosition(Camera.main)?.transform;

                if (selectedObjectTransform != null && selectedObjectTransform.GetComponent<IDraggable>() != null 
                                                    && selectedObjectTransform.GetComponent<IDraggable>().IsDraggingEnabled())
                {
                    selectedObject = selectedObjectTransform.GetComponent<IDraggable>();
                }

                if (touchStart != Vector3.zero)
                {
                    touchStarted = true;
                }
                else
                {
                    touchStarted = false;
                }
            }else if (Input.GetMouseButton(0) && touchStarted)
            {
                var direction = touchStart - Utils.GetMouseClickHit(Camera.main);

                if (selectedObject == null)
                {
                    //Just move the camera
                    transform.position += direction;
                }
                else
                {
                    selectedObject.UpdatePosition(new Vector3(Mathf.RoundToInt(Utils.GetMouseClickHit(Camera.main).x), Mathf.RoundToInt(Utils.GetMouseClickHit(Camera.main).y)));
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
            
            if (selectedObject != null)
            {
                var lerpVector = Vector3.Lerp(transform.position, selectedObject.GetTransform().position, 20 * Time.deltaTime);
                transform.position = new Vector3(lerpVector.x, lerpVector.y, transform.position.z);
            }
        }
    }
}
