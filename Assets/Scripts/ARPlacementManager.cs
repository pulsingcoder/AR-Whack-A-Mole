using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ARPlacementManager : MonoBehaviour
{
    ARRaycastManager m_ARRaycastManager;
    static List<ARRaycastHit> raycast_Hits = new List<ARRaycastHit>();
    public Camera arCamera;
    public GameObject objectToPlace;
    public GameObject planeDetectionVideo;
    public GameObject tapObjectToPlaceVideo;
    public TextMeshProUGUI infoText;
    public Vector3 positionToPlace;
    public GameObject infoGameObject;
    public GameObject gameManager;
   
    // Start is called before the first frame update
    void Start()
    {
        m_ARRaycastManager = GetComponent<ARRaycastManager>();
        planeDetectionVideo.SetActive(true);
        tapObjectToPlaceVideo.SetActive(false);
        gameManager.SetActive(false);
        infoText.text = "Move phone to detect plane";
        positionToPlace = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = arCamera.ScreenPointToRay(centerOfScreen);
        if (m_ARRaycastManager.Raycast(ray, raycast_Hits, TrackableType.PlaneWithinPolygon))
        {
            planeDetectionVideo.SetActive(false);
            tapObjectToPlaceVideo.SetActive(true);
            infoText.text = "Tap to place object";
            Pose hitPose = raycast_Hits[0].pose;
            positionToPlace = hitPose.position;
        
        }
    }

    public void PlaceObject()
    {
        if (objectToPlace)
        {
            infoGameObject.SetActive(false);
            gameManager.SetActive(true);
            tapObjectToPlaceVideo.SetActive(false);
            objectToPlace.transform.position = positionToPlace;
        }
      
    }
}
