using System.Collections.Generic;
using UnityEngine;

namespace Figure
{
    [CreateAssetMenu(fileName = "Figure Database", menuName = "Figures/Figure Database")]
    public class FigureDatabase : ScriptableObject
    {
        public List<FigureData> figures;
    }
}