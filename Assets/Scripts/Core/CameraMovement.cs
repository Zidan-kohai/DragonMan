using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float aheadDistanseX;
    [SerializeField] float aheadDistanseY;
    [SerializeField] float cameraSpeed;
    private float lookAheadX;
    private float lookAheadY;

    private void Update()
    {
        transform.position = new Vector3(player.position.x + lookAheadX, player.position.y + lookAheadY, transform.position.z);
        lookAheadX = Mathf.Lerp(lookAheadX, (aheadDistanseX * player.localScale.x), Time.deltaTime * cameraSpeed);
        lookAheadY = Mathf.Lerp(lookAheadY, aheadDistanseY, Time.deltaTime * cameraSpeed);
    }
}