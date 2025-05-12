using UnityEngine;

namespace Figure
{
    public class FigureSpawner : MonoBehaviour
    {
        [SerializeField] private FigureDatabase database;
        [SerializeField] private GameObject figurePrefab;
        [SerializeField] private Transform spawnParent;
        
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