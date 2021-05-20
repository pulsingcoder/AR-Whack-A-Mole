using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARShoot : MonoBehaviour
{
    public Camera arCamera;
    public GameObject explosionPrefab;
    RaycastHit hit;
    
   // public GameObject hammer;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var touchZero = Input.GetTouch(0);
        if (Input.touchCount > 0 && touchZero.phase == TouchPhase.Began)
        {
            Ray ray =  arCamera.ScreenPointToRay(touchZero.position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Mole")
                {
                    if (hit.transform.position.y > -2.8)
                    {
                       
                        if (hit.transform.GetChild(11))
                        {
                            hit.transform.GetChild(11).gameObject.SetActive(true);
                        }
                        
                        StartCoroutine(DeactiveMoleAfterSecond(hit.transform.gameObject));
                    }
                }
            }
            //if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit ))
            //{
            //  //  Instantiate(hammer, arCamera.transform.position + new Vector3(0, 0, 0.2f), Quaternion.identity); 
            //    if (hit.transform.tag == "Mole")
            //    {
            //        Destroy(hit.transform.gameObject);
            //    }

            //}
        }
    }

    IEnumerator DeactiveMoleAfterSecond(GameObject mole)
    {
        
        yield return new WaitForSeconds(0.3f);
        mole.transform.GetChild(11).gameObject.SetActive(false);
        mole.SetActive(false);
        explosionPrefab.GetComponent<ParticleSystem>().Play();


    }
}
