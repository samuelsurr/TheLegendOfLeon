using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBallSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public Transform player;
    public float spawnRangeX = 10f;  // Horizontal range within which the objects will spawn
    public float spawnHeight = 10f;  // Height from which the objects will spawn
    public float moveSpeed = 5f;     // Speed at which the objects move downwards
    public float spawnInterval = 2f; // Time interval between spawns

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        while (true)
        {
            // Randomly determine the horizontal position within the specified range
            float spawnX = Random.Range(player.position.x - spawnRangeX, player.position.x + spawnRangeX);
            Vector3 spawnPosition = new Vector3(spawnX, player.position.y + spawnHeight, 0f);
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

            // Make the object a trigger so it can go through walls
            Collider2D collider = spawnedObject.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.isTrigger = true;
            }

            // Move the object straight down
            StartCoroutine(MoveObjectDown(spawnedObject));

            // Wait for the specified spawn interval before spawning the next object
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator MoveObjectDown(GameObject obj)
    {
        while (obj != null)
        {
            // Move the object straight down
            obj.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
    }
}
