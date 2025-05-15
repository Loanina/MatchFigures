using Core;
using UnityEngine;

namespace Figure.Types.Sticky
{
    public class StickyBehaviour : IFigureBehaviour
    {
        public void OnSpawn(FigureView view, IGameEvents gameEvents)
        {
            var shapeObject = view.GetShapeObject();

            var rb = shapeObject.GetComponent<Rigidbody2D>();
            if (rb == null)
                rb = shapeObject.AddComponent<Rigidbody2D>();

            var sticky = shapeObject.AddComponent<StickyTrigger>();
            sticky.Init(rb);
        }
    }
}