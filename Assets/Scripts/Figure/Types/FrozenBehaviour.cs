using Bar;
using Core;

namespace Figure.Types
{
    public class FrozenBehaviour : IFigureBehaviour
    {
      //  private bool isFrozen = true;

        public void OnSpawn(FigureView view)
        {
            view.SetInteractable(false);
            GameManager.Instance.OnFigureUnfrozen += () =>
            {
                //isFrozen = false;
                view.SetInteractable(true);
            };
        }
        
        public void OnClick(FigureView view) { }
    }
}