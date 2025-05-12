using UnityEngine;

namespace Figure
{
    [CreateAssetMenu(fileName = "New Figure", menuName = "Figures/Figure")]
    public class FigureData : ScriptableObject
    {
        public Sprite icon;
        public Sprite shape;
        public Color backgroundColor;
    }
}