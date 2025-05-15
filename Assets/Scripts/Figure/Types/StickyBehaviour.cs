namespace Figure.Types
{
    public class StickyBehaviour : IFigureBehaviour
    {
        public void OnSpawn(FigureView view) { }

        public void OnClick(FigureView view)
        {
            //поставить enable у  var joint = gameObject.AddComponent<FixedJoint2D>();  ps FigureView
        }
    }
}