using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public SignalSender playerHealthSignal;
    public FloatValue currentHealth;
    [SerializeField] private SimpleFlash flashEffect;
    private PlayerMovement playerMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (currentHealth.RuntimeValue <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fireball"))
        {
            currentHealth.RuntimeValue -= 1;
            flashEffect.Flash();
            playerHealthSignal.Raise();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            currentHealth.RuntimeValue -= 1;
            flashEffect.Flash();
            playerHealthSignal.Raise();
        }
    }

    private void Die()
    {
        if (currentHealth.RuntimeValue <= 0)
        {
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("death");
            playerMovement.enabled = false;
            gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
            
            StartCoroutine(RestartLevel());
        }
    }

    private IEnumerator RestartLevel()
    {
        // Wait for a specific duration (e.g., 2 seconds)
        yield return new WaitForSeconds(2.0f);
        gameObject.layer = LayerMask.NameToLayer("Default");
        // Restart the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        currentHealth.RuntimeValue = currentHealth.initialValue; // Reset health to initial value
    }
}
