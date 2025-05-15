using Core;
using UnityEngine;

namespace Figure.Types
{
    public class FrozenBehaviour : IFigureBehaviour
    { 
        public void OnSpawn(FigureView view, IGameEvents gameEvents)
        {
            view.SetInteractable(false);
            var fx = view.GetFrozenEffect();
            if (fx != null)
            {
                var effect = Object.Instantiate(fx, view.GetShapeObject().transform);
                view.SetVisible(false);

                void Unfreeze()
                {
                    view.SetInteractable(true);
                    Object.Destroy(effect);
                    view.SetVisible(true);
                    gameEvents.OnFigureUnfrozen -= Unfreeze;
                }

                gameEvents.OnFigureUnfrozen += Unfreeze;
            }
        }
    }
}