using System.Collections.Generic;
using Core;
using Figure;
using UnityEngine;

namespace Bar
{
    public class BarManager : MonoBehaviour
    {
        [SerializeField] private Transform barParent;
        [SerializeField] private BarFigureView barFigurePrefab;

        private readonly List<BarFigureView> currentFigures = new();
        private readonly Dictionary<string, List<BarFigureView>> figureGroups = new();

        private const int MaxFigures = 7;
        private const int MatchCount = 3;

        public bool TryAddFigure(FigureData data)
        {
            if (currentFigures.Count >= MaxFigures)
                return false;

            var barFigure = Instantiate(barFigurePrefab, barParent);
            barFigure.Setup(data.shape, data.backgroundColor, data.icon);
            currentFigures.Add(barFigure);

            string key = GetKey(data);
            if (!figureGroups.ContainsKey(key))
                figureGroups[key] = new List<BarFigureView>();
            figureGroups[key].Add(barFigure);

            if (figureGroups[key].Count == MatchCount)
            {
                var toRemove = data.type == FigureType.Explosive
                    ? GetFiguresWithNeighbors(figureGroups[key])
                    : new List<BarFigureView>(figureGroups[key]);

                RemoveFigures(toRemove);
                figureGroups.Remove(key);
            }

            if (currentFigures.Count >= MaxFigures)
                GameManager.Instance.HandleLose();

            return true;
        }

        private void RemoveFigures(List<BarFigureView> figures)
        {
            foreach (var fig in figures)
            {
                currentFigures.Remove(fig);
                Destroy(fig.gameObject);
            }
        }

        private List<BarFigureView> GetFiguresWithNeighbors(List<BarFigureView> matched)
        {
            HashSet<int> indices = new();
            foreach (var fig in matched)
            {
                int index = currentFigures.IndexOf(fig);
                if (index == -1) continue;

                indices.Add(index);
                if (index > 0) indices.Add(index - 1);
                if (index < currentFigures.Count - 1) indices.Add(index + 1);
            }

            var result = new List<BarFigureView>();
            foreach (int i in indices)
                if (i >= 0 && i < currentFigures.Count)
                    result.Add(currentFigures[i]);

            return result;
        }

        private string GetKey(FigureData data)
        {
            string colorKey = $"{data.backgroundColor.r:F2}_{data.backgroundColor.g:F2}_{data.backgroundColor.b:F2}_{data.backgroundColor.a:F2}";
            return $"{data.shape.name}_{data.icon.name}_{colorKey}";
        }

        public void ClearBar()
        {
            foreach (var fig in currentFigures)
                if (fig != null) Destroy(fig.gameObject);

            currentFigures.Clear();
            figureGroups.Clear();
        }

        public BarFigureView GetFigure(int index) => currentFigures[index];
        public int FigureIndexOf(BarFigureView view) => currentFigures.IndexOf(view);
        public int GetFigureCount() => currentFigures.Count;
    }
}