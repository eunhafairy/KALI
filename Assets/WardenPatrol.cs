using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenPatrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    private float startTime;

    public Transform[] moveSpots;
    private int randomSpot;
    Transform moveSpot;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isWalking", true);
        moveSpots = new Transform[11];
        moveSpot = GameObject.Find("FieldWardenMoveSpots").transform;
        Debug.Log(moveSpot.childCount);
        for (int x = 0; x < moveSpot.childCount; x++)
        {

            moveSpots[x] = moveSpot.GetChild(x);

        }
        speed = 2;
        startTime = Random.Range(7, 9);
        waitTime = startTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

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
