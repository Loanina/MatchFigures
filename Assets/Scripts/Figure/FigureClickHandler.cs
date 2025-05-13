using Bar;
using System.Collections.Generic;
using UnityEngine;

namespace Figure
{
    public class FigureClickHandler : MonoBehaviour
    {
        [SerializeField] private BarManager barManager;
        [SerializeField] private FigureSpawner spawner;

        private List<FigureView> trackedFigures = new();

        public void TrackFigure(FigureView figure)
        {
            trackedFigures.Add(figure);
            figure.FigureClicked += OnFigureClicked;
        }

        private void OnFigureClicked(FigureView figure)
        {
            bool added = barManager.TryAddFigure(
                figure.GetShapeSprite(), 
                figure.GetBackgroundColor(), 
                figure.GetIconSprite()
            );

            if (!added) return;

            trackedFigures.Remove(figure);
            spawner.UnregisterFigure(figure.gameObject);
            Destroy(figure.gameObject);
        }

        private void OnDestroy()
        {
            foreach (var figure in trackedFigures)
                figure.FigureClicked -= OnFigureClicked;
        }
    }
}