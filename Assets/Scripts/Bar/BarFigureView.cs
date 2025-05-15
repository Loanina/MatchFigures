using UnityEngine;
using UnityEngine.UI;

namespace Bar
{
    public class BarFigureView : MonoBehaviour
    {
        [SerializeField] private Image shape;
        [SerializeField] private Image icon;

        public void Setup(Sprite shapeSprite, Color backgroundColor, Sprite iconSprite)
        {
            shape.sprite = shapeSprite;
            icon.sprite = iconSprite;
            shape.color = backgroundColor;
        }
    }
}