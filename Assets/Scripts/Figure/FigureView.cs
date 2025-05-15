using System;
using Core;
using Core.Input;
using Figure.Types;
using Figure.Types.Sticky;
using UnityEngine;

namespace Figure
{
    public class FigureView : MonoBehaviour, IClickable
    {
        [SerializeField] private SpriteRenderer shapeRenderer;
        [SerializeField] private SpriteRenderer iconRenderer;
        private IFigureBehaviour _behaviour;
        private bool isInteractable = true;
        private GameObject activeEffect;
        private FigureData data;
        
        public event Action<FigureView> FigureClicked;
        
        public void OnClick()
        {
            if (!isInteractable) return;
            FigureClicked?.Invoke(this);
        }

        public void Setup(FigureData data)
        {
            this.data = data;
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
            switch (type)
            {
                case FigureType.Heavy:
                    _behaviour = new HeavyBehaviour();
                    break;
                case FigureType.Sticky:
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

        public FigureData GetFigureData() => data;

        public void SetInteractable(bool isInteractable) => this.isInteractable = isInteractable;
        public void SpawnFrozenEffect()
        {
            var frozenEffectPrefab = GameManager.Instance.GetFrozenEffect();
            if (frozenEffectPrefab == null)
            {
                Debug.LogWarning("FrozenEffectPrefab is not assigned!");
                return;
            }
            activeEffect = Instantiate(frozenEffectPrefab, shapeRenderer.transform);
            SetVisibility(false);
        }

        public void DestroyFrozenEffect()
        {
            if (activeEffect != null)
            {
                SetVisibility(true);
                Destroy(activeEffect);
            }
        }

        private void SetVisibility(bool isVisible)
        {
            shapeRenderer.enabled = isVisible;
            iconRenderer.enabled = isVisible;
        }
        
        public GameObject GetShapeObject() => shapeRenderer.gameObject;
    }
}