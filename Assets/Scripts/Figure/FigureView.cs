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
        private bool isInteractable = true;
        private IGameEvents gameEvents;

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

        public void OnClick()
        {
            if (!isInteractable) return;
            FigureClicked?.Invoke(this);
        }

        public FigureData GetFigureData() => data;

        public void SetInteractable(bool state) => isInteractable = state;

        public GameObject GetShapeObject() => shapeRenderer.gameObject;
        public GameObject GetFrozenEffect() => frozenEffectPrefab;

        public void SetVisible(bool isVisible)
        {
            shapeRenderer.enabled = isVisible;
            iconRenderer.enabled = isVisible;
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
