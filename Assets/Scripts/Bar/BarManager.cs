using System.Collections.Generic;
using UnityEngine;

namespace Bar
{
    public class BarManager : MonoBehaviour
    {
        [SerializeField] private Transform barParent;
        [SerializeField] private GameObject barFigurePrefab;

        private List<GameObject> currentFigures = new();
        private const int MaxFigures = 7;

        public bool TryAddFigure(Sprite shapeSprite, Color backgroundColor, Sprite iconSprite)
        {
            var figure = Instantiate(barFigurePrefab, barParent);
            figure.GetComponent<BarFigureView>().Setup(shapeSprite, backgroundColor, iconSprite);
            currentFigures.Add(figure);
            
            if (currentFigures.Count >= MaxFigures)
            {
                Debug.Log("Проигрыш! Бар заполнен.");
                return false;
            }
            return true;
        }
    }
}