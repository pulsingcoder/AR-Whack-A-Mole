using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class ARShoot : MonoBehaviour
{
    Camera arCamera;
    public GameObject scorePopUp;
    
    PhotonView photonView;
    public int score = 0;
 
    RaycastHit hit;
    AudioSource myAudioSource;
    
   // public GameObject hammer;
 
    // Start is called before the first frame update
    void Start()
    {
        arCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        myAudioSource = GetComponent<AudioSource>();
        /// scorePopUp = GameObject.FindGameObjectWithTag("PopUpNumber");
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

        if (photonView.IsMine)
        {
            if (Input.touchCount > 0)
            {
                var touchZero = Input.GetTouch(0);
                if (Input.touchCount > 0 && touchZero.phase == TouchPhase.Began)
                {
                    Ray ray = arCamera.ScreenPointToRay(touchZero.position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.tag == "Mole")
                        {
                            if (hit.transform.position.y > -2.8)
                            {
                                myAudioSource.Play();
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
        }
    }

    IEnumerator DeactiveMoleAfterSecond(GameObject mole)
    {
        
        yield return new WaitForSeconds(0.3f);
        mole.transform.GetChild(11).gameObject.SetActive(false);
        mole.SetActive(false);
       // hit.transform.GetChild(12).gameObject.SetActive(true);
        Instantiate(scorePopUp, mole.transform.position + new Vector3(0,0.2f,0), Quaternion.Euler(new Vector3(0, 180, 0)));
   
        score += 10;
        if (photonView.IsMine)
        {
           
            print("At Shoot ");
            GameObject.FindGameObjectWithTag("ScoreManager").gameObject.GetComponent<ScoreManager>().UpdateScore(score);
        }

    }


}
