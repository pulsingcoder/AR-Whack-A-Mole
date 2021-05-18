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
