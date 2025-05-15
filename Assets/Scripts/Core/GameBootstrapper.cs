using Bar;
using Figure;
using UnityEngine;

namespace Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BarManager barManager;
        [SerializeField] private FigureSpawner figureSpawner;
        [SerializeField] private GameUIController uiController;

        private void Awake()
        {
            var clickHandler = new FigureClickHandler(barManager, figureSpawner, gameManager);
            gameManager.Init(clickHandler, figureSpawner, barManager, uiController);
            barManager.Init(gameManager);
            figureSpawner.Init(clickHandler, gameManager);
        }
    }
}