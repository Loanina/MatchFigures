using TMPro;
using UnityEngine;

namespace Core
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private GameObject winLabel;
        [SerializeField] private GameObject loseLabel;
        [SerializeField] private TextMeshProUGUI scoreTMP;

        public void ShowWinLabel() => winLabel.SetActive(true);
        public void ShowLoseLabel() => loseLabel.SetActive(true);
        public void HideAllLabels()
        {
            winLabel.SetActive(false);
            loseLabel.SetActive(false);
        }

        public void UpdateScore(int score)
        {
            scoreTMP.text = "Score: " + score;
        }
    }
}