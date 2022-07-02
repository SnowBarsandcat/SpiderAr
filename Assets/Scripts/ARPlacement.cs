using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    public GameObject arObjectToSpawn;
    public GameObject placementIndicator;
    private GameObject spawnedObject;
    // public GameObject crosshair;
    private Pose placementPose;
    private ARRaycastManager arRaycatManager;
    private bool placementPoseIsValid = false;

    public GameObject joystickCanvas;
    public GameObject pointSpawner;

    private float initialDistance;
    private Vector3 initialScale;

    // private Shoot shoot;
    
    // Start is called before the first frame update
    void Start()
    {
        arRaycatManager = FindObjectOfType<ARRaycastManager>();
        joystickCanvas.SetActive(false);
        pointSpawner.SetActive(false);
        //crosshair.SetActive(false);
        //shoot = GetComponent<Shoot>();
        //shoot.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedObject == null && 
           placementPoseIsValid && 
           Input.touchCount > 0 && 
           Input.GetTouch(0).phase == TouchPhase.Began)
        {

            ARPlaceObject();
            joystickCanvas.SetActive(true);
            pointSpawner.SetActive(true);
            //shoot.enabled = true;
            //crosshair.SetActive(true);
        }

        /*if(spawnedObject == null *//*&& shoot.enabled == true*//*)
        {
            //shoot.enabled = false;
            crosshair.SetActive(false);
        }*/

        ApplyingFingerScale();

        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    void ApplyingFingerScale ()
    {
        if (Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Canceled 
                || touchZero.phase == TouchPhase.Ended
                || touchOne.phase == TouchPhase.Canceled 
                || touchOne.phase == TouchPhase.Ended)
            {
                return;
            }

            if (touchZero.phase == TouchPhase.Began 
                || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(
                                                touchZero.position, 
                                                touchOne.position);
                initialScale = spawnedObject.transform.localScale;
            }
            else
            {
                float currentDistance = Vector2.Distance(
                                                touchZero.position,
                                                touchOne.position);
                
                if (Mathf.Approximately(initialDistance, 0))
                {
                    return;
                }

                float factor = currentDistance / initialDistance;
                spawnedObject.transform.localScale = initialScale * factor;
            }


        }
    }

    void UpdatePlacementIndicator()
    {
        if(spawnedObject == null && placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
            
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        arRaycatManager.Raycast(screenCenter, hits, TrackableType.Planes);
        placementPoseIsValid = hits.Count > 0;
        if(placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    void ARPlaceObject()
    {
        spawnedObject = Instantiate(arObjectToSpawn, placementPose.position, placementPose.rotation);
    }
}
