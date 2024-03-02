using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Something : MonoBehaviour
{
    [Range(0f, 1f)]
    public float value;

    public Transform Point1;
    public Transform Point2;
    public Material material1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(Point1.position, Point2.position, value);

        material1.color = Color.Lerp(Color.green, Color.red, value);
        
    }
}
