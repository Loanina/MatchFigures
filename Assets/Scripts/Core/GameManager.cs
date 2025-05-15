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
        [SerializeField] private FigureSpawner spawner;
        private FigureClickHandler clickHandler;
        [SerializeField] public BarManager barManager;
        [SerializeField] private GameObject winLabel;
        [SerializeField] private GameObject loseLabel;
        [Range(1, 30), SerializeField] private uint requiredToUnfreeze = 5;
        [SerializeField] private TextMeshProUGUI scoreTMP;
        [SerializeField] private GameObject frozenFigureEffect;
        private Coroutine spawnCoroutine;
        private int removedFigureCount = 0;

        public static GameManager Instance { get; private set; }
        public event Action OnFigureUnfrozen;

        public void Init(FigureClickHandler clickHandler)
        {
            this.clickHandler = clickHandler;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
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

        public void HandleLose()
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

        private List<BarFigureView> GetNeighbors(BarFigureView center)
        {
            var neighbors = new List<BarFigureView>();
            var index =  barManager.FigureIndexOf(center);

            if (index == -1)
                return neighbors;

            if (index > 0)
                neighbors.Add(barManager.GetFigure(index - 1));

            if (index < barManager.GetFigureCount() - 1)
                neighbors.Add(barManager.GetFigure(index + 1));

            return neighbors;
        }
        
        public void DeleteNeighbors(BarFigureView center)
        {
            var neighbors = GetNeighbors(center);
            foreach (var neighbor in neighbors)
                Destroy(neighbor.gameObject);
        }

        public GameObject GetFrozenEffect() => frozenFigureEffect;
    }
}