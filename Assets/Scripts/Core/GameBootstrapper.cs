using Bar;
using Figure;
using UnityEngine;

namespace Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private FigureClickHandler clickHandler;
        [SerializeField] private BarManager barManager;
        [SerializeField] private FigureSpawner figureSpawner;

        private void Awake()
        {
            clickHandler.Init(barManager, figureSpawner, gameManager);
            figureSpawner.Init(clickHandler);
        }
    }
}