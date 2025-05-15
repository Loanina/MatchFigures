using System;
using Bar;
using Figure;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour, IGameEvents
    {
        [Range(1, 30)] [SerializeField] private uint requiredToUnfreeze = 5;
        private BarManager barManager;
        private FigureClickHandler clickHandler;
        private int removedFigureCount;
        private Coroutine spawnCoroutine;
        private FigureSpawner spawner;
        private GameUIController ui;

        private void Awake()
        {
            RestartGame();
        }

        public event Action OnFigureUnfrozen;

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

        public void Init(FigureClickHandler clickHandler, FigureSpawner spawner, BarManager barManager,
            GameUIController ui)
        {
            this.clickHandler = clickHandler;
            this.spawner = spawner;
            this.barManager = barManager;
            this.ui = ui;
        }

        public void RestartGame()
        {
            ClearGame();

            if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);

            spawnCoroutine = StartCoroutine(spawner.SpawnAllFigures());
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
    }
}