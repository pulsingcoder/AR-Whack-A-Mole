using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARPlacementAndPlaneDetectionControlller : MonoBehaviour
{
    ARPlacementManager m_ARPlacementManager;
    ARPlaneManager m_ARPlaneManager;
    public GameObject placeButton;
    public GameObject adjustButton;
    private float initialTouchDistance;
    private Vector3 initialScale;
    
    
    


    private void Awake()
    {
        m_ARPlacementManager = GetComponent<ARPlacementManager>();
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
      
    }

    // Start is called before the first frame update
    void Start()
    {

        placeButton.SetActive(true);
        adjustButton.SetActive(false);
           
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && Input.touchCount > 0)
            {
                m_ARPlacementManager.PlaceObject();

                DisableARPlacementAndPlaneDetection();

            }
            // Scale object
            // We'll using the touch count
            if (Input.touchCount == 2)
            {
                var touchZero = Input.GetTouch(0);
                var touchOne = Input.GetTouch(1);
                if (touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled ||
                    touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled)
                {
                    return;
                }
                if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                {
                    initialTouchDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    initialScale = m_ARPlacementManager.objectToPlace.transform.localScale;
                   
                }
                else
                {
                    var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    if (Mathf.Approximately(currentDistance, 0))
                    {
                        return;
                    }
                    else
                    {
                        var factor = currentDistance / initialTouchDistance;
                        m_ARPlacementManager.objectToPlace.transform.localScale = initialScale * factor;
                    }
                }

            }
        }
    }


    public void DisableARPlacementAndPlaneDetection()
    {
        m_ARPlaneManager.enabled = false;
        m_ARPlacementManager.enabled = false;
        placeButton.SetActive(false);
        adjustButton.SetActive(true);
        SetAllPlanesActiveOrDetective(false);


    }


    public void EnableARPlacementAndPlaneDetection()
    {
        m_ARPlaneManager.enabled = true;
        m_ARPlacementManager.enabled = true;
        placeButton.SetActive(true);
        adjustButton.SetActive(false);
        SetAllPlanesActiveOrDetective(true);
    }




    void SetAllPlanesActiveOrDetective(bool value)
    {
        foreach(var plane in m_ARPlaneManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }

    
}
