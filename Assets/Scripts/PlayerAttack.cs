using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public FloatValue maxHealthforEnemy;

    private Rigidbody2D enemyRB;
    [SerializeField] private SimpleFlash flashEffect;
    [SerializeField] private KeyCode flashKey;
    public GameObject enemy;


    private void Awake()
    {
       
  
        
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            maxHealthforEnemy.RuntimeValue -= 1;
          
            flashEffect.Flash();


            if (maxHealthforEnemy.RuntimeValue == 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        
        enemy.layer = LayerMask.NameToLayer("IgnoreCollision");
        //anim.SetTrigger("dead");
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(2.0f);
        enemy.SetActive(false);
    }
}
