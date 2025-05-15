using System;
using Core.Input;
using Figure.Types;
using UnityEngine;

namespace Figure
{
    public class FigureView : MonoBehaviour, IClickable
    {
        [SerializeField] private SpriteRenderer shapeRenderer;
        [SerializeField] private SpriteRenderer iconRenderer;
        [SerializeField] public SpriteRenderer frozenRenderer;
        private IFigureBehaviour _behaviour;
        private bool isInteractable = true;
        
        public event Action<FigureView> FigureClicked;
        
        public void OnClick()
        {
            if (!isInteractable) return;
            _behaviour?.OnClick(this);
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
            
            ApplyBehavior(data.type);
        }
        
        private void ApplyBehavior(FigureType type)
        {
            var rb = GetComponentInChildren<Rigidbody2D>();
            switch (type)
            {
                case FigureType.Heavy:
                    _behaviour = new HeavyBehaviour();
                    break;
                case FigureType.Sticky:
                    var joint = gameObject.AddComponent<FixedJoint2D>();
                    joint.enabled = false;
                    _behaviour = new StickyBehaviour();
                    break;
                case FigureType.Explosive:
                    _behaviour = new ExplosiveBehaviour();
                    break;
                case FigureType.Frozen:
                    _behaviour = new FrozenBehaviour(); 
                    break;
            }
            _behaviour?.OnSpawn(this);
        }
        
        public Sprite GetShapeSprite() => shapeRenderer.sprite;
        public Color GetBackgroundColor() => shapeRenderer.color;
        public Sprite GetIconSprite() => iconRenderer.sprite;

        public void SetInteractable(bool isInteractable) => this.isInteractable = isInteractable;
    }
}