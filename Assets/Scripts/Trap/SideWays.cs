using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWays : MonoBehaviour
{
    [SerializeField] private float moveDistanse;
    [SerializeField] private float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - moveDistanse;
        rightEdge = transform.position.x + moveDistanse;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if(transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else 
                movingLeft = false;
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else 
                movingLeft = true;
        }
    }

}
