using Core;

namespace Figure.Types
{
    public class FrozenBehaviour : IFigureBehaviour
    {
        public void OnSpawn(FigureView view)
        {
            view.SetInteractable(false);
            GameManager.Instance.OnFigureUnfrozen += () =>
            {
                view.SetInteractable(true);
                view.DisableIcon();
            };
        }
        
        public void OnClick(FigureView view) { }
    }
}