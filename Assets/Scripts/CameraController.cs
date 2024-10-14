using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [FormerlySerializedAs("ZoomSpeed")]
        [Range(10, 150)]
        [Tooltip("Speed of zooming camera")]
        public float zoomSpeed = 50.0f;
        private Camera _camera;
        public void Start()
        {
            _camera = GetComponent<Camera>();
        }

        public void Update()
        {
            _camera.orthographicSize = Math.Max(1.5f,
                _camera.orthographicSize + Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed);
            //_camera.orthographicSize += Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed;
        }
    }
}