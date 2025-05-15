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
        [SerializeField] private GameObject frozenEffectPrefab;
        private IFigureBehaviour behaviour;
        private FigureData data;
        private IGameEvents gameEvents;
        private bool isInteractable = true;

        public void OnClick()
        {
            if (!isInteractable) return;
            FigureClicked?.Invoke(this);
        }

        public event Action<FigureView> FigureClicked;

        public void Setup(FigureData data, IGameEvents gameEvents)
        {
            this.gameEvents = gameEvents;
            this.data = data;

            SetupVisuals();
            SetupCollider();

            behaviour = CreateBehaviour(data.type);
            behaviour?.OnSpawn(this, gameEvents);
        }

        public FigureData GetFigureData()
        {
            return data;
        }

        public void SetInteractable(bool state)
        {
            isInteractable = state;
        }

        public GameObject GetShapeObject()
        {
            return shapeRenderer.gameObject;
        }

        public GameObject GetFrozenEffect()
        {
            return frozenEffectPrefab;
        }

        public void SetVisible(bool isVisible)
        {
            if (shapeRenderer != null) shapeRenderer.enabled = isVisible;
            if (iconRenderer != null) iconRenderer.enabled = isVisible;
        }

        private void SetupVisuals()
        {
            shapeRenderer.sprite = data.shape;
            shapeRenderer.color = data.backgroundColor;
            iconRenderer.sprite = data.icon;
        }

        private void SetupCollider()
        {
            var collider = shapeRenderer.GetComponent<PolygonCollider2D>();
            if (collider == null)
                collider = shapeRenderer.gameObject.AddComponent<PolygonCollider2D>();
            else
                collider.pathCount = 0;
        }

        private IFigureBehaviour CreateBehaviour(FigureType type)
        {
            return type switch
            {
                FigureType.Heavy => new HeavyBehaviour(),
                FigureType.Sticky => new StickyBehaviour(),
                FigureType.Explosive => new ExplosiveBehaviour(),
                FigureType.Frozen => new FrozenBehaviour(),
                _ => null
            };
        }
    }
}