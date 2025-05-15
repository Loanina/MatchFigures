using System;
using Bar;
using Figure;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour, IGameEvents
    {
        private FigureSpawner spawner;
        private FigureClickHandler clickHandler;
        private BarManager barManager;
        private GameUIController ui;

        [Range(1, 30), SerializeField] private uint requiredToUnfreeze = 5;
        private Coroutine spawnCoroutine;
        private int removedFigureCount;

        public event Action OnFigureUnfrozen;

        public void Init(FigureClickHandler clickHandler, FigureSpawner spawner, BarManager barManager, GameUIController ui)
        {
            this.clickHandler = clickHandler;
            this.spawner = spawner;
            this.barManager = barManager;
            this.ui = ui;
        }

        private void Awake()
        {
            RestartGame();
        }

        public void RestartGame()
        {
            ClearGame();

            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }

            spawnCoroutine = StartCoroutine(spawner.SpawnAllFigures());
        }

        public void OnWin()
        {
            Debug.Log("Победа!");
            ui.ShowWinLabel();
        }

        public void OnLose()
        {
            Debug.Log("Проигрыш!");
            ui.ShowLoseLabel();
        }

        private void ClearGame()
        {
            ui.HideAllLabels();

            spawner.ClearAllFigures();
            clickHandler.Clear();
            barManager.ClearBar();

            removedFigureCount = 0;
            ui.UpdateScore(removedFigureCount);
        }

        public void OnFigureRemoved()
        {
            removedFigureCount++;
            ui.UpdateScore(removedFigureCount);

            if (removedFigureCount == requiredToUnfreeze)
            {
                Debug.Log("Unfreezing frozen figures...");
                OnFigureUnfrozen?.Invoke();
                OnFigureUnfrozen = null;
            }
        }
    }
}
