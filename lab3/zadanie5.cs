using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public int numberOfCubes = 10;
    public float planeSize = 10f;

    private void Start()
    {
        HashSet<Vector2> usedPositions = new HashSet<Vector2>();

        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 spawnPosition;

            do
            {
                float x = Random.Range(-planeSize / 2, planeSize / 2);
                float z = Random.Range(-planeSize / 2, planeSize / 2);
                spawnPosition = new Vector3(x, 0.5f, z);
            } while (usedPositions.Contains(new Vector2(spawnPosition.x, spawnPosition.z)));

            usedPositions.Add(new Vector2(spawnPosition.x, spawnPosition.z));

            Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
