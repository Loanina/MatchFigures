using System;
using Core.Input;
using UnityEngine;

namespace Figure
{
    public class FigureView : MonoBehaviour, IClickable
    {
        [SerializeField] private SpriteRenderer shapeRenderer;
        [SerializeField] private SpriteRenderer iconRenderer;
        
        public event Action<FigureView> FigureClicked;
        
        public void OnClick()
        {
            FigureClicked?.Invoke(this);
        }

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
        
        public Sprite GetShapeSprite() => shapeRenderer.sprite;
        public Color GetBackgroundColor() => shapeRenderer.color;
        public Sprite GetIconSprite() => iconRenderer.sprite;
    }
}