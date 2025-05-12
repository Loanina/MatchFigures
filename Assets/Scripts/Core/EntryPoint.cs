using Figure;
using UnityEngine;

namespace Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private FigureSpawner figureSpawner;

        private void Start()
        {
            StartCoroutine(figureSpawner.SpawnAllFigures());
        }
    }
}