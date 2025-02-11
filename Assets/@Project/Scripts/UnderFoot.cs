using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderFoot : MonoBehaviour
{
    public float bounce;
    public Rigidbody2D rb2D;

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the tag of the collider or the collider's parent is "Trampy"
        if (other.CompareTag("Trampy"))
        {
            // Destroy(other.gameObject);
            rb2D.velocity = new Vector2(rb2D.velocity.x, bounce);
        } else if (other.CompareTag("Mushroom"))
        {
            // Destroy(other.gameObject);
            rb2D.velocity = new Vector2(rb2D.velocity.x, bounce);
        }
    }
}
