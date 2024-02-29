using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        hit = new RaycastHit();
        ray = new Ray(transform.position, target.position - transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(ray, out hit);
        Debug.DrawLine(ray.origin, hit.point, Color.red);
    }
}
