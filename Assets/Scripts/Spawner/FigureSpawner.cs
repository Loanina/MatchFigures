using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Figure
{
    public class FigureSpawner : MonoBehaviour
    {
        [SerializeField] private FigureDatabase database;
        [SerializeField] private GameObject figurePrefab;
        [SerializeField] private Transform spawnParent;
        [Range(0, 10)] [SerializeField] private float spawnDelay = 0.5f;
        private FigureClickHandler clickHandler;
        private IGameEvents gameEvents;
        private readonly List<GameObject> spawnedFigures = new();

        public void Init(FigureClickHandler handler, IGameEvents gameEvents)
        {
            clickHandler = handler;
            this.gameEvents = gameEvents;
        }

        public IEnumerator SpawnAllFigures()
        {
            var allFigures = new List<FigureData>(database.figures);
            List<FigureData> spawnQueue = new();

            foreach (var figure in allFigures)
                for (var i = 0; i < 3; i++)
                    spawnQueue.Add(figure);

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
            view.Setup(data, gameEvents);
            spawnedFigures.Add(figure);
            clickHandler.TrackFigure(view);
        }

        public void UnregisterFigure(GameObject figure)
        {
            spawnedFigures.Remove(figure);
        }

        private void Shuffle<T>(List<T> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var randIndex = Random.Range(i, list.Count);
                (list[i], list[randIndex]) = (list[randIndex], list[i]);
            }
        }

        public void ClearAllFigures()
        {
            foreach (var obj in spawnedFigures)
                if (obj != null)
                    Destroy(obj);
            spawnedFigures.Clear();
        }
    }
}