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
        private List<BarFigureView> currentFigures = new List<BarFigureView>();
        private Dictionary<string, List<BarFigureView>> figureGroups = new();
        private const int MaxFigures = 7;
        private const int MatchCount = 3;

        public bool TryAddFigure(FigureData data)
        {
            if (currentFigures.Count >= MaxFigures)
                return false;

            var obj = Instantiate(barFigurePrefab, barParent);
            var barFigureView = obj.GetComponent<BarFigureView>();
                barFigureView.Setup(data.shape, data.backgroundColor, data.icon);
            currentFigures.Add(barFigureView);

            var key = GetFigureKey(data.shape, data.icon, data.backgroundColor);

            if (!figureGroups.ContainsKey(key))
                figureGroups[key] = new List<BarFigureView>();

            figureGroups[key].Add(barFigureView);
            
            if (figureGroups[key].Count == MatchCount)
            {
                if (data.type == FigureType.Explosive)
                {
                    var explodedFigures = GetFiguresWithNeighbors(figureGroups[key]);
                    foreach (var fig in explodedFigures)
                    {
                        currentFigures.Remove(fig);
                        Destroy(fig.gameObject);
                    }
                    figureGroups.Remove(key);
                }
                else
                {
                    foreach (var fig in figureGroups[key])
                    {
                        Destroy(fig.gameObject);
                        currentFigures.Remove(fig);
                    }
                    figureGroups.Remove(key);
                }
            }
            
            if (currentFigures.Count == MaxFigures)
                GameManager.Instance.HandleLose();

            return true;
        }

        private List<BarFigureView> GetFiguresWithNeighbors(List<BarFigureView> sourceFigures)
        {
            HashSet<int> uniqueIndices = new();

            foreach (var fig in sourceFigures)
            {
                var index = currentFigures.IndexOf(fig);
                if (index == -1) continue;

                uniqueIndices.Add(index);

                if (index > 0)
                    uniqueIndices.Add(index - 1);

                if (index < currentFigures.Count - 1)
                    uniqueIndices.Add(index + 1);
            }

            List<BarFigureView> result = new();
            foreach (var index in uniqueIndices)
            {
                if (index >= 0 && index < currentFigures.Count)
                    result.Add(currentFigures[index]);
            }

            return result;
        }

        private string GetFigureKey(Sprite shape, Sprite icon, Color color)
        {
            var colorKey = $"{color.r:F2}_{color.g:F2}_{color.b:F2}_{color.a:F2}";
            return $"{shape.name}_{icon.name}_{colorKey}";
        }
        
        public void ClearBar()
        {
            foreach (var barFigureView in currentFigures)
            {
                if (barFigureView != null)
                    Destroy(barFigureView.gameObject);
            }
            currentFigures.Clear();
            figureGroups.Clear();
        }

        public int GetCountOfFigures() => currentFigures.Count;

        public BarFigureView GetFigure(int indexOf) => currentFigures[indexOf];
        public int FigureIndexOf(BarFigureView barFigureView) => currentFigures.IndexOf(barFigureView);
        public int GetFigureCount() => currentFigures.Count;
    }
}