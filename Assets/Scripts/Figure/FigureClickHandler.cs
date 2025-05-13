using Bar;
using System.Collections.Generic;
using Core;
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
            CheckFiguresExist();
        }

        private void CheckFiguresExist()
        {
            if (trackedFigures.Count != 0) return;
            GameManager.Instance.HandleWin();
        }

        private void OnDestroy()
        {
            foreach (var figure in trackedFigures)
                figure.FigureClicked -= OnFigureClicked;
        }
    }
}