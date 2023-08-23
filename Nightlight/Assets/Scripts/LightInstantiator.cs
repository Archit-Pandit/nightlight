using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject lightPrefab;

    [SerializeField] private Vector2 spawnBorders = new Vector2(100f, 100f);

    [SerializeField] private int limit = 5;

    private WaitForSeconds spawnDelay = new WaitForSeconds(1f);

    private Coroutine spawning = null;

    private List<GameObject> lightObjs = new List<GameObject>();

    private float halfBorderX;
    private float halfBorderY;

    // Start is called before the first frame update
    void Start()
    {
        halfBorderX = spawnBorders.x / 2;
        halfBorderY = spawnBorders.y / 2;

        StartSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        if (lightObjs.Count >= limit)
        {
            StopSpawning();
        }
        else if (spawning == null)
        {
            StartSpawning();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnBorders);
    }

    private void StartSpawning()
    {
        if (spawning != null) { StopCoroutine(spawning); }

        spawning = StartCoroutine(SpawnLights(spawnDelay));
    }

    private void StopSpawning()
    {
        StopCoroutine(spawning);

        //spawning = null;
    }

    private IEnumerator SpawnLights(WaitForSeconds delay)
    {
        while (true)
        {
            float randomPointX = Random.Range(-halfBorderX, halfBorderX);
            float randomPointY = Random.Range(-halfBorderY, halfBorderY);

            Vector2 spawnPoint = new Vector2(randomPointX, randomPointY);

            GameObject lightObj = Instantiate(lightPrefab, spawnPoint, Quaternion.identity);

            lightObjs.Add(lightObj);

            yield return delay;
        }
    }
}
