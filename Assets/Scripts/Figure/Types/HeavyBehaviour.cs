using Core;
using UnityEngine;

namespace Figure.Types
{
    public class HeavyBehaviour : IFigureBehaviour
    {
        public void OnSpawn(FigureView view, IGameEvents gameEvents)
        {
            var rb = view.GetComponentInChildren<Rigidbody2D>();
            if (rb != null)
                rb.gravityScale *= 4f;
        }
    }
}