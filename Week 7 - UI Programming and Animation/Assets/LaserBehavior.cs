using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    LineRenderer lineRenderer;
    public Transform start, end;
    private float rayDistance;
    public Gradient defaultGradient, hitGradient;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rayDistance = Vector3.Distance(start.position, end.position);
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(start.position, start.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.transform.tag == "Player")
            {
                Debug.DrawRay(start.position, ray.direction * hit.distance, Color.yellow);
                lineRenderer.colorGradient = hitGradient;
            }
            else
            {
                Debug.DrawRay(start.position, ray.direction * rayDistance, Color.white);
                lineRenderer.colorGradient = defaultGradient;
            }
        }        
    }
}
