using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    ScoreController scoreController;
    public GameObject explosion;

    private void Awake()
    {
        scoreController = GameObject
                            .FindGameObjectWithTag("Controller")
                            .GetComponent<ScoreController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.transform.tag == "Spider")
        {
            scoreController.score++;
            GameObject createdExplosion = Instantiate(
                                                explosion, 
                                                transform.position, 
                                                transform.rotation);
            createdExplosion.transform.localScale = transform.localScale;
            Destroy(gameObject);
            Destroy(createdExplosion, 2f);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
