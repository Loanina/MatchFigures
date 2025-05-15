using UnityEngine;

namespace Figure.Types
{
    public class StickyBehaviour : IFigureBehaviour
    {
        public void OnSpawn(FigureView view)
        {
            var joint = view.gameObject.AddComponent<FixedJoint2D>();
            joint.enabled = true;
        }

        public void OnClick(FigureView view) { }
    }
}