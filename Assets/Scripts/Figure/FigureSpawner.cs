using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Figure
{
    public class FigureSpawner : MonoBehaviour
    {
        [SerializeField] private FigureDatabase database;
        [SerializeField] private FigureClickHandler clickHandler;
        [SerializeField] private GameObject figurePrefab;
        [SerializeField] private Transform spawnParent;
        [SerializeField] private float spawnDelay = 0.5f;
        
        private List<GameObject> spawnedFigures = new();
        
        public IEnumerator SpawnAllFigures()
        {
            var allFigures = new List<FigureData>(database.figures);
            List<FigureData> spawnQueue = new();
            
            foreach (var figure in allFigures)
            {
                for (var i = 0; i < 3; i++)
                    spawnQueue.Add(figure);
            }

            Shuffle(spawnQueue);

            foreach (var figureData in spawnQueue)
            {
                SpawnFigure(figureData);
                yield return new WaitForSeconds(spawnDelay);
            }
        }
        
        private void SpawnFigure(FigureData data)
        {
            var figure = Instantiate(figurePrefab, spawnParent);
            var view = figure.GetComponent<FigureView>();
            view.Setup(data);
            spawnedFigures.Add(figure);
            clickHandler.TrackFigure(view);
        }
        
        public void UnregisterFigure(GameObject figure)
        {
            spawnedFigures.Remove(figure);
        }

        private void Shuffle<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int randIndex = Random.Range(i, list.Count);
                (list[i], list[randIndex]) = (list[randIndex], list[i]);
            }
        }
        
        public void SpawnFigure(int index)
        {
            if (index < 0 || index >= database.figures.Count) return;

            var data = database.figures[index];
            var instance = Instantiate(figurePrefab, spawnParent);
            instance.GetComponent<FigureView>().Setup(data);
        }

        public void SpawnRandom()
        {
            var index = Random.Range(0, database.figures.Count);
            SpawnFigure(index);
        }
    }
}