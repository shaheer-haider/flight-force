using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPlayerMove : MonoBehaviour
{

    private CharacterController controller;
    public float fowardSpeed = 10.0f;
    [Range(0f, 10f)]
    public float rotSpeedX = 3.0f;
    [Range(0f, 10f)]
    public float rotSpeedY = 1.5f;
    [Range(0f, 80f)]
    public float clampedXLowestValue;
    [Range(280f, 360f)]
    public float clampedXHighestValue;
    [Range(0f, 180f)]
    public float clampedYLowestValue;
    [Range(180f, 360f)]
    public float clampedYHighestValue;
    public bool clampOn;
    private Dictionary<int, Vector2> activeTouches = new Dictionary<int, Vector2>();


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

        if (gameObject != null)
        {
            // give foward vel
            Vector3 moveVector = transform.forward * fowardSpeed;

            Vector3 inputs = GetPlayerInput();

            Vector3 yaw = inputs.x * transform.right * rotSpeedX * Time.deltaTime;
            Vector3 pitch = inputs.y * transform.up * rotSpeedY * Time.deltaTime;
            Vector3 dir = yaw + pitch;

            float maxX = Quaternion.LookRotation(moveVector + dir).eulerAngles.x;
            float maxY = Quaternion.LookRotation(moveVector + dir).eulerAngles.y;
            if (maxX > clampedXLowestValue && maxX < clampedXHighestValue && clampOn || maxY > clampedYLowestValue && maxY < clampedYHighestValue && clampOn || maxX > 80 && maxX < 280)
            {

            }
            else
            {
                moveVector += dir;

                transform.rotation = Quaternion.LookRotation(moveVector);
            }

            controller.Move(moveVector * Time.deltaTime);

        }


    }






    public Vector3 GetPlayerInput()
    {


        Vector3 r = Vector3.zero;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                activeTouches.Add(touch.fingerId, touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (activeTouches.ContainsKey(touch.fingerId))
                {
                    activeTouches.Remove(touch.fingerId);
                }

            }

            else
            {
                float mag = 0;
                r = (touch.position - activeTouches[touch.fingerId]);
                mag = r.magnitude / 300;
                r = r.normalized * mag;
            }
        }
        return r;

    }




}

