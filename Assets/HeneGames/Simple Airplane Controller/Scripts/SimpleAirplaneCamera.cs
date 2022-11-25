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
            bool isJoyStickMoving = joystick.Vertical != 0 || joystick.Horizontal != 0;
            float _deltatime = Time.deltaTime;
            if (freeLook.m_Lens.FieldOfView != _fov)
            {
                freeLook.m_Lens.FieldOfView = Mathf.Lerp(freeLook.m_Lens.FieldOfView, _fov, _deltatime * 5f);
            }
            
        }
    }
}