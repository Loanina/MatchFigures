using System.Collections.Generic;
using Bar;
using Core;
using UnityEngine;

namespace Figure
{
    public class FigureClickHandler
    {
        private readonly BarManager barManager;
        private readonly IGameEvents gameEvents;
        private readonly FigureSpawner spawner;
        private readonly List<FigureView> trackedFigures = new();

        public FigureClickHandler(BarManager barManager, FigureSpawner spawner, IGameEvents gameEvents)
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
            Object.Destroy(figure.gameObject);
            CheckFiguresExist();
            gameEvents.OnFigureRemoved();
        }

        private void CheckFiguresExist()
        {
            if (trackedFigures.Count != 0) return;
            gameEvents.OnWin();
        }

        public void Clear()
        {
            foreach (var fig in trackedFigures)
                fig.FigureClicked -= OnFigureClicked;

            trackedFigures.Clear();
        }
    }
}