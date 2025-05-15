using Bar;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Figure
{
    public class FigureClickHandler : MonoBehaviour
    {
        private BarManager barManager;
        private FigureSpawner spawner;
        private IGameEvents gameEvents;
        private List<FigureView> trackedFigures = new();

        public void Init(BarManager barManager, FigureSpawner spawner, IGameEvents gameEvents)
        {
            this.barManager = barManager;
            this.spawner = spawner;
            this.gameEvents = gameEvents;
        }
        
        public void TrackFigure(FigureView figure)
        {
            trackedFigures.Add(figure);
            figure.FigureClicked += OnFigureClicked;
        }

        private void OnFigureClicked(FigureView figure)
        {
            var added = barManager.TryAddFigure(figure.GetFigureData());
            if (!added) return;

            trackedFigures.Remove(figure);
            spawner.UnregisterFigure(figure.gameObject);
            Destroy(figure.gameObject);
            CheckFiguresExist();
            gameEvents.OnFigureRemoved();
        }

        private void CheckFiguresExist()
        {
            if (trackedFigures.Count != 0) return;
            gameEvents.OnWin();
        }

        private void OnDestroy()
        {
            foreach (var figure in trackedFigures)
                figure.FigureClicked -= OnFigureClicked;
        }
        
        public void Clear()
        {
            foreach (var fig in trackedFigures)
                fig.FigureClicked -= OnFigureClicked;

            trackedFigures.Clear();
        }
    }
}