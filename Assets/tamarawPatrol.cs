using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tamarawPatrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    private float startTime;

    public Transform[] moveSpots;
    private int randomSpot;
    Transform moveSpot;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isWalking", true);
        moveSpot = GameObject.Find("TamarawMove").transform;
        moveSpots = new Transform[18];
        Debug.Log(moveSpot.childCount);
        for (int x = 0; x < moveSpot.childCount; x++)
        {

            moveSpots[x] = moveSpot.GetChild(x);

        }
        startTime = Random.Range(8, 12);
        waitTime = startTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }


    void FixedUpdate()
    {

        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if (transform.position.x > moveSpots[randomSpot].position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else {
            GetComponent<SpriteRenderer>().flipX = true;


        }

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {

            if (waitTime <= 0)
            {
                animator.SetBool("isWalking", true);

                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startTime;
            }
            else
            {
                animator.SetBool("isWalking", false);
                waitTime -= Time.deltaTime;
            }

        }

    }
}
