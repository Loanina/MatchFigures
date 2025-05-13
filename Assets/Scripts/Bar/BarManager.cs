using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Bar
{
    public class BarManager : MonoBehaviour
    {
        [SerializeField] private Transform barParent;
        [SerializeField] private GameObject barFigurePrefab;

        private List<GameObject> currentFigures = new();
        private Dictionary<string, List<BarFigureView>> figureGroups = new();
        private const int MaxFigures = 7;
        private const int MatchCount = 3;

        public bool TryAddFigure(Sprite shapeSprite, Color backgroundColor, Sprite iconSprite)
        {
            if (currentFigures.Count >= MaxFigures)
                return false;

            var obj = Instantiate(barFigurePrefab, barParent);
            var barFigureView = obj.GetComponent<BarFigureView>();
                barFigureView.Setup(shapeSprite, backgroundColor, iconSprite);
            currentFigures.Add(obj);

            var key = GetFigureKey(shapeSprite, iconSprite, backgroundColor);

            if (!figureGroups.ContainsKey(key))
                figureGroups[key] = new List<BarFigureView>();

            figureGroups[key].Add(barFigureView);
            
            if (figureGroups[key].Count == MatchCount)
            {
                foreach (var fig in figureGroups[key])
                {
                    Destroy(fig.gameObject);
                    currentFigures.Remove(fig.gameObject);
                }
                figureGroups.Remove(key);
            }
            
            if (currentFigures.Count == MaxFigures)
                GameManager.Instance.HandleLose();

            return true;
        }

        private string GetFigureKey(Sprite shape, Sprite icon, Color color)
        {
            var colorKey = $"{color.r:F2}_{color.g:F2}_{color.b:F2}_{color.a:F2}";
            return $"{shape.name}_{icon.name}_{colorKey}";
        }
        
        public void ClearBar()
        {
            foreach (var obj in currentFigures)
            {
                if (obj != null)
                    Destroy(obj);
            }
            currentFigures.Clear();
            figureGroups.Clear();
        }
    }
}