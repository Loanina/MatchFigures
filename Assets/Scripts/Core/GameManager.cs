using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject winLabel;
        [SerializeField] private GameObject loseLabel;

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
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

        public void Restart()
        {
            winLabel.SetActive(false);
            loseLabel.SetActive(false);
        }
    }
}