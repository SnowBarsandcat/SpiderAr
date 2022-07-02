using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private GameObject arCamera;
    public GameObject explosion;

    RaycastHit hit;
    
    void Start()
    {
        arCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 
            && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Physics.Raycast(
                arCamera.transform.position, 
                arCamera.transform.forward,
                out hit))
            {
                if (hit.transform.tag == "Spider")
                {
                    Destroy(hit.transform.gameObject);
                    GameObject createdExplosion = Instantiate(
                            explosion,
                            hit.transform.position,
                            hit.transform.rotation
                        );
                    Destroy(createdExplosion, 2f);
                }


            }
        }
    }
}
