using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RabbitMovement : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Animator rabbitAnimator;

    private Vector3 startPosition;
    private Vector3 randomDestination;
    private float idleForThisTime;    
    private float walkForThisTime;

    public float walkSpeed;
    public Vector2 travelRadius;
    public Vector2 idleTime; 
    public Vector2 walkTime;
    public bool isWalking = false;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        rabbitAnimator = GetComponent<Animator>();

        startPosition = transform.position;
        randomDestination = startPosition;

        Walk();
    }

    private void Update()
    {
        if (navAgent.velocity.magnitude != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        rabbitAnimator.SetBool("isMoving", isWalking);
    }


    private void FindNewDestination()
    {
        float radiusOfTravelX = Random.Range(travelRadius.x, travelRadius.y);
        float radiusOfTravelZ = Random.Range(travelRadius.x, travelRadius.y);
        randomDestination = new Vector3(startPosition.x + radiusOfTravelX, startPosition.y, startPosition.z + radiusOfTravelZ);

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDestination, out navHit, 2f, -1);

        navAgent.SetDestination(navHit.position);
        randomDestination = startPosition;
    }

    private void Idle()
    {
        navAgent.isStopped = true;
        navAgent.speed = 0f;

        idleForThisTime = Random.Range(idleTime.x, idleTime.y);
      
        StartCoroutine(IdlingTime(idleForThisTime));
    }

    IEnumerator IdlingTime(float time)
    {
        yield return new WaitForSeconds(time);
        Walk();
    }   

    private void Walk()
    {
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;

        FindNewDestination();

        walkForThisTime = Random.Range(walkTime.x, walkTime.y);
       
        StartCoroutine(WalkingTime(walkForThisTime));
    }

    IEnumerator WalkingTime(float time)
    {
        yield return new WaitForSeconds(time);
        Idle();
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the start position
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, 10f);
    }

}
