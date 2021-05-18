using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARShoot : MonoBehaviour
{
    public GameObject arCamera;
    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (hit.transform.name == "rat")
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
