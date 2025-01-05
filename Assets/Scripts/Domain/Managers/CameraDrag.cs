using Domain.Utilities;
using Game.Interfaces;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 2.0f; // Velocidad de arrastre
    public Vector2 minBounds; // Límite inferior del mapa (X, Z)
    public Vector2 maxBounds; // Límite superior del mapa (X, Z)

    private Vector3 dragOrigin;
    private bool isDragging = false;

    void Update()
    {
        // Detecta cuando se presiona el botón izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Utils.GetMouseClickAtFloor();

            // Raycast para detectar si el clic inicial fue sobre un collider
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var draggable = hit.transform.GetComponent<IDraggable>();
                if (draggable is not null && draggable.IsDraggingEnabled())
                {
                    draggable.Interact();
                    isDragging = false;
                    return;
                }
            }

            // Si no hay collider, habilitamos el arrastre
            isDragging = true;
        }

        // Si no se mantiene el botón izquierdo o no se está arrastrando, no hace nada
        if (!Input.GetMouseButton(0) || !isDragging) return;

        // Calcula la diferencia entre la posición inicial y actual del ratón
        Vector2 difference = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
        Vector2 move = new Vector3(difference.x * dragSpeed, difference.y * dragSpeed);

        // Mueve la cámara
        transform.Translate(move, Space.World);

        // Aplica los límites para la posición de la cámara
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
        clampedPosition.y = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
        transform.position = clampedPosition;

        // Actualiza el origen del arrastre
        dragOrigin = Input.mousePosition;
    }
}
