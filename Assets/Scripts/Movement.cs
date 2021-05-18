using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Joystick joystick;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (joystick.Vertical > 0)
        {
            rb.AddForce(Vector3.forward * 50f );
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
