using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour
{
    public Fireball fireball;
    public Transform castsource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(fireball, castsource.position, castsource.rotation);
        }
    }
}
