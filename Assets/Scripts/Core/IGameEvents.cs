using System;

namespace Core
{
    public interface IGameEvents
    {
        void OnFigureRemoved();
        void OnWin();
        void OnLose();
        event Action OnFigureUnfrozen;
    }
}