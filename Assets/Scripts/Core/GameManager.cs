using System;
using System.Collections.Generic;
using Bar;
using Figure;
using TMPro;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour, IGameEvents
    {
        private FigureSpawner spawner;
        private FigureClickHandler clickHandler;
        public BarManager barManager;
        [SerializeField] private GameObject winLabel;
        [SerializeField] private GameObject loseLabel;
        [Range(1, 30), SerializeField] private uint requiredToUnfreeze = 5;
        [SerializeField] private TextMeshProUGUI scoreTMP;
        private Coroutine spawnCoroutine;
        private int removedFigureCount = 0;
        public event Action OnFigureUnfrozen;

        public void Init(FigureClickHandler clickHandler, FigureSpawner spawner, BarManager barManager)
        {
            this.clickHandler = clickHandler;
            this.spawner = spawner;
            this.barManager = barManager;
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
                spawnCoroutine = null;
            }
            spawnCoroutine = StartCoroutine(spawner.SpawnAllFigures());
        }

        public void OnWin()
        {
            Debug.Log("Победа!");
            winLabel.SetActive(true);
        }

        public void OnLose()
        {
            Debug.Log("Проигрыш!");
            loseLabel.SetActive(true);
        }
        
        private void ClearGame()
        {
            winLabel.SetActive(false);
            loseLabel.SetActive(false);

            spawner.ClearAllFigures();
            clickHandler.Clear();
            barManager.ClearBar();
            removedFigureCount = 0;
            scoreTMP.text = "Score: " + removedFigureCount;
        }
    
        public void OnFigureRemoved()
        {
            removedFigureCount++;
            scoreTMP.text = "Score: " + removedFigureCount;

            if (removedFigureCount >= requiredToUnfreeze)
            {
                Debug.Log("Unfreezing frozen figures...");
                OnFigureUnfrozen?.Invoke();
                OnFigureUnfrozen = null;
            }
        }
    }
}