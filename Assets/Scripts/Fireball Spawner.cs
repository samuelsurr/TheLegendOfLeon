using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireballSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public Transform player;
    public float spawnRangeX = 10f;
    public float spawnHeight = 5f;
    public float moveSpeed = 5f;
    public float spawnInterval = 2f; // Time interval between spawns

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        while (true)
        {
            // Spawn at the right side within the specified range
            float spawnX = spawnRangeX;
            Vector3 spawnPosition = new Vector3(spawnX, spawnHeight, 0f);
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

            // Make the object a trigger so it can go through walls
            Collider2D collider = spawnedObject.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.isTrigger = true;
            }

            // Move the object in a straight line towards the player's initial position
            Vector3 targetPosition = new Vector3(player.position.x, spawnPosition.y, 0f);
            StartCoroutine(MoveObjectToTarget(spawnedObject, targetPosition));

            // Wait for the specified spawn interval before spawning the next object
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator MoveObjectToTarget(GameObject obj, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - obj.transform.position).normalized;

        while (obj != null)
        {
            // Move the object
            obj.transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

            yield return null;
        }
    }
}