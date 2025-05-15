using Core;
using UnityEngine;

namespace Figure.Types
{
    public class FrozenBehaviour : IFigureBehaviour
    { 
        public void OnSpawn(FigureView view)
        {
            view.SetInteractable(false);
            var fx = GameManager.Instance.GetFrozenEffect();
            if (fx != null)
            {
                var effect = Object.Instantiate(fx, view.GetShapeObject().transform);
                view.SetVisible(false);

                GameManager.Instance.OnFigureUnfrozen += () =>
                {
                    view.SetInteractable(true);
                    Object.Destroy(effect);
                    view.SetVisible(true);
                };
            }
        }
    }
}