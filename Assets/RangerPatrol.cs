using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerPatrol : MonoBehaviour
{

    public float speed;
    private float waitTime;
    private float startTime;

    public Transform[] moveSpots;
    private int randomSpot;
    Transform moveSpot;
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isWalking", true);
        moveSpots = new Transform[8];
        moveSpot = GameObject.Find("FieldRangerMoveSpots").transform;
        Debug.Log(moveSpot.childCount);
        for (int x = 0; x < moveSpot.childCount; x++) {

            moveSpots[x] = moveSpot.GetChild(x);

        }
        startTime = Random.Range(4,6);
        waitTime = startTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime) ;

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f) {

            if (waitTime <= 0)
            {
                animator.SetBool("isWalking", true);

                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startTime;
            }
            else {
                animator.SetBool("isWalking",false);
                waitTime -= Time.deltaTime;
            }

        }


    }
}
