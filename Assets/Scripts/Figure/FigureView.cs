using UnityEngine;

namespace Figure
{
    public class FigureView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer shapeRenderer;
        [SerializeField] private SpriteRenderer iconRenderer;

        public void Setup(FigureData data)
        {
            shapeRenderer.sprite = data.shape;
            shapeRenderer.color = data.backgroundColor;

            iconRenderer.sprite = data.icon;
            
            var collider = shapeRenderer.GetComponent<PolygonCollider2D>();
            if (collider == null)
                collider = shapeRenderer.gameObject.AddComponent<PolygonCollider2D>();
            else
                collider.pathCount = 0;
        }
    }
}