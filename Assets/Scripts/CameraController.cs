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
        [Range(10, 200)]
        [Tooltip("Speed of zooming camera")]
        public float zoomSpeed = 100.0f;
        
        [FormerlySerializedAs("ScrollSpeed")]
        [Range(10, 2000)]
        [Tooltip("Speed of scrolling map")]
        public float scrollSpeed = 100.0f;
        private Camera _camera;
        public void Start()
        {
            _camera = GetComponent<Camera>();
        }

        public void SetSize(float size)
        {
            _camera.orthographicSize = size;
        }
        public void Update()
        {
            _camera.orthographicSize = Math.Max(1.5f,
                _camera.orthographicSize + (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl) ? 1.0f : 0) * Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed);
            _camera.transform.position = new Vector3(_camera.transform.position.x + (Input.GetKey(KeyCode.LeftShift) ? 1.0f : 0) * Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed, _camera.transform.position.y + (Input.GetKey(KeyCode.LeftControl) ? 1.0f : 0) * Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed,
                _camera.transform.position.z);
            //_camera.orthographicSize += Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed;
        }
    }
}