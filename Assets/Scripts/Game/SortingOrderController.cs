using Game.Interfaces;
using UnityEngine;

namespace Game
{
    public class SortingOrderController : MonoBehaviour
    {
        [SerializeField]
        private int orderMultiplier;

        private SpriteRenderer[] spriteRenderers;
        private SpriteRenderer referenceSpriteRender;

        private void Awake()
        {
            spriteRenderers = transform.GetComponentsInChildren<SpriteRenderer>();
            referenceSpriteRender = spriteRenderers[0];
        }

        void Start()
        {
            SortLayers();
        }

        private void SortLayers()
        {
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.sortingOrder = (int)(transform.position.y * orderMultiplier);
            }
        }

        private void LateUpdate()
        {
            int sortingLayer = (int)(transform.position.y * orderMultiplier);

            if (sortingLayer == referenceSpriteRender.sortingOrder)
            {
                return;
            }
            
            SortLayers();
        }
    }
}
