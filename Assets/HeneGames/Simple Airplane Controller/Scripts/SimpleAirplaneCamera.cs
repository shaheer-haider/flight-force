using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
namespace HeneGames.Airplane
{
    public class SimpleAirplaneCamera : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SimpleAirPlaneController airPlaneController;
        [SerializeField] private CinemachineFreeLook freeLook;
        [Header("Camera values")]
        [SerializeField] private float cameraDefaultFov = 60f;
        [SerializeField] private float cameraTurboFov = 40f;
        Touch touch;

        public Joystick joystick;

        private void Start()
        {
            //Lock and hide mouse
            // Cursor.lockState = CursorLockMode.Locked;
            // Cursor.visible = false;
            // freeLook.enabled = false;
        }

        private void Update()
        {
            CameraFovUpdate();
        }

        private void CameraFovUpdate()
        {
            //Turbo
            if (!airPlaneController.PlaneIsDead())
            {
                if (airPlaneController.isTurboActive)
                {
                    ChangeCameraFov(cameraTurboFov);
                }
                else
                {
                    ChangeCameraFov(cameraDefaultFov);
                }
            }
        }

        public void ChangeCameraFov(float _fov)
        {
            float _deltatime = Time.deltaTime;

            if (Input.touchCount > 0)
            {
                // don't do anything if the touch is on a UI element
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || joystick.Vertical != 0 || joystick.Horizontal != 0)
                    return;

                // move camera with the touch
                freeLook.m_XAxis.Value += Input.GetTouch(Input.touchCount - 1).deltaPosition.x * _deltatime;
                freeLook.m_YAxis.Value += Input.GetTouch(Input.touchCount - 1).deltaPosition.y * _deltatime;
            }
        }
    }
}