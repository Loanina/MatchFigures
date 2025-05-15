namespace Figure.Types
{
    public interface IFigureBehaviour
    {
        void OnSpawn(FigureView view);
        void OnClick(FigureView view);
    }
}