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
            Debug.Assert(shape != null, "Shape Image is not assigned");
            Debug.Assert(icon != null, "Icon Image is not assigned");

            shape.sprite = shapeSprite;
            shape.color = backgroundColor;
            icon.sprite = iconSprite;
        }

        public void Clear()
        {
            shape.sprite = null;
            icon.sprite = null;
        }
    }
}