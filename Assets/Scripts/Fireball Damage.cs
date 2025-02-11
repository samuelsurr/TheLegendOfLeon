using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDamage : MonoBehaviour
{
    public GameObject fireBall;
    private Rigidbody2D rb;
    public SignalSender playerHealthSignal;
    public FloatValue playerCurrentHealth;
   
    public FloatValue maxHealthforEnemy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
                if (collision.CompareTag("Boundary"))
                {
                    Destroy(collision);
                }
    }
}
