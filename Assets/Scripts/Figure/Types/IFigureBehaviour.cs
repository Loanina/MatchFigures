using Core;

namespace Figure.Types
{
    public interface IFigureBehaviour
    {
        void OnSpawn(FigureView view, IGameEvents gameEvents);
    }
}