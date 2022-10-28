using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFlightCamFollow : MonoBehaviour
{

    public Transform lookAt;
    public GameObject player;
    private Vector3 desiredPosition;
    public float offset = 1.5f;
    public float distance = 3.5f;

    private void Update()
    {
        if (player != null)
        {
            desiredPosition = lookAt.position + (-transform.forward * distance) + (transform.up * offset);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);

            transform.LookAt(lookAt.position + (Vector3.up * offset));
        }
    }



}
