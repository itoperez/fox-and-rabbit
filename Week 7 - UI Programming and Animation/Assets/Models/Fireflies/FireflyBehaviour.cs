using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FireflyBehaviour : MonoBehaviour
{
    // reference to the firefly's animator
    public Animator anim;
    public float speedModifier;
    public float delayModifier;

    // flight path
    public Vector2 flightRadiusVer, flightRadiusHor;
    public Vector2 FlightTimeSec, InPlaceTimeSec;
    private Vector3 startPosition;
    private bool isFlying;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("Speed", speedModifier);
        anim.SetFloat("Delay", delayModifier);

        startPosition = transform.position;
        isFlying = false;
    }

    private void Update()
    {
        if (!isFlying)
        {
            StartCoroutine(fly());
        }
    }

    IEnumerator fly()
    {
        isFlying = true;        

        Vector3 startPosition = transform.position;
        Vector3 flightPosition = startPosition;
        Vector3 targetPosition = RandomVector(flightRadiusVer, flightRadiusHor);

        float timeElapsed = 0;
        float flightTime = Random.Range(FlightTimeSec.x, FlightTimeSec.y);

        while (timeElapsed < flightTime)
        {
            flightPosition.x = Mathf.Lerp(startPosition.x, targetPosition.x, timeElapsed / flightTime);
            flightPosition.y = Mathf.Lerp(startPosition.y, targetPosition.y, timeElapsed / flightTime);
            flightPosition.z = Mathf.Lerp(startPosition.z, targetPosition.z, timeElapsed / flightTime);

            transform.position = flightPosition;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(Random.Range(InPlaceTimeSec.x, InPlaceTimeSec.y));
        isFlying = false;
        //Debug.Log("Made it to the end of fly()");
    }

    private Vector3 RandomVector(Vector2 vertical, Vector2 horizontal)
    {
        var x = Random.Range(horizontal.x, horizontal.y) * Random.Range(0, 2) * 2 - 1;
        var y = Random.Range(vertical.x, vertical.y) * Random.Range(0, 2) * 2 - 1;
        var z = Random.Range(horizontal.x, horizontal.y) * Random.Range(0, 2) * 2 - 1;

        return new Vector3(startPosition.x + x, startPosition.y + y, startPosition.z + z);
    }

}