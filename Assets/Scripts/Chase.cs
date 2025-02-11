using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Chase : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public Transform target;//transform in my deduction just means to change state in animation. Like if player is in radius change animation to walk towards them
    public float chaseRadius;
    public float attackRadius;
    public float moveSpeed;
    private void Start()
    {

        myRigidbody = GetComponent<Rigidbody2D>(); //referencing to rigid body
        target = GameObject.FindWithTag("Player").transform; //transform holds the location informati
    }
    void FixedUpdate()
    {
        CheckDistance();

    }
    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)//we need to look for distance between two things whenever we do Vector3.Distance
        {

                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                myRigidbody.MovePosition(temp);

        }
 
    }

}
