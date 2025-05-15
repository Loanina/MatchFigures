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

        private void Awake()
        {
            var clickHandler = new FigureClickHandler(barManager, figureSpawner, gameManager);
            gameManager.Init(clickHandler, figureSpawner, barManager);
            barManager.Init(gameManager);
            figureSpawner.Init(clickHandler, gameManager);
        }
    }
}