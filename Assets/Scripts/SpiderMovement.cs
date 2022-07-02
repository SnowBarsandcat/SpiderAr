using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpiderMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform gamePad;
    public float moveSpeed = 0.5f;

    GameObject arObject;
    Vector3 move;

    bool walking;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.localPosition = Vector2.ClampMagnitude(
                                            eventData.position - (Vector2)gamePad.position, 
                                            gamePad.rect.width * 0.5f);

        move = new Vector3(transform.localPosition.x, 0f, transform.localPosition.y).normalized;
        if(!walking)
        {
            walking = true;
            arObject.GetComponent<Animator>().SetBool("Walk", true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(PlayerMovement());
    }

    IEnumerator PlayerMovement()
    {
        while(true)
        {
            arObject.transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

            if(move != Vector3.zero)
            {
                arObject.transform.rotation = Quaternion.Slerp(
                    arObject.transform.rotation,
                    Quaternion.LookRotation(move),
                    Time.deltaTime * 5f);
                yield return null;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        move = Vector3.zero;
        StopCoroutine(PlayerMovement());
        walking = false;
        arObject.GetComponent<Animator>().SetBool("Walk", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        arObject = GameObject.FindGameObjectWithTag("Spider");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
