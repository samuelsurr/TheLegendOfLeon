using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public enum CloudType { SmallCloud, BigCloud } // Enum for cloud types
    public CloudType cloudType; // Cloud type variable

    public float speed = 3f; // Speed of the cloud

    private float spriteWidth; // Width of the cloud sprite

    void Start()
    {
        // Get the width of the sprite
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Move the cloud along the X axis
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (cloudType == CloudType.SmallCloud)
        {
            // If the cloud moves out of the screen, reset it to the right side of the screen
            if (transform.position.x < -Camera.main.orthographicSize * Camera.main.aspect - spriteWidth / 2)
            {
                transform.position = new Vector3(Camera.main.orthographicSize * Camera.main.aspect + spriteWidth / 2, transform.position.y, transform.position.z);
            }
        }
        else if (cloudType == CloudType.BigCloud)
        {
            // If the cloud moves out of the screen, move it to the right side of the previous cloud
            if (transform.position.x < -Camera.main.orthographicSize * Camera.main.aspect - spriteWidth)
            {
                transform.position = new Vector3(transform.position.x + 2 * spriteWidth, transform.position.y, transform.position.z);
            }
        }
    }
}
