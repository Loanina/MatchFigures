using Core;

namespace Figure.Types
{
    public class FrozenBehaviour : IFigureBehaviour
    {
        public void OnSpawn(FigureView view)
        {
            view.SetInteractable(false);
            view.SpawnFrozenEffect();

            GameManager.Instance.OnFigureUnfrozen += () =>
            {
                view.SetInteractable(true);
                view.DestroyFrozenEffect();
            };
        }
        
        public void OnClick(FigureView view) { }
    }
}