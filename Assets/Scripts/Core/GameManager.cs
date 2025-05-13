using Bar;
using Figure;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private FigureSpawner spawner;
        [SerializeField] private FigureClickHandler clickHandler;
        [SerializeField] private BarManager barManager;
        [SerializeField] private GameObject winLabel;
        [SerializeField] private GameObject loseLabel;
        private Coroutine spawnCoroutine;

        public static GameManager Instance { get; private set; }

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

        public void HandleWin()
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
        }
    }
}