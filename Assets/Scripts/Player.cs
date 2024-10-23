using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _color = 2;
    public int Color => _color;
    [SerializeField] private float _moveSpeed = 5;
    private int _score = 0;
    private Renderer _renderer;
    public int Score => _score;
    
    public event Action OnScoreChanged;
    public void Initialize(int playerColor)
    {
        _color = playerColor;
        _renderer = GetComponent<Renderer>();
        _renderer.sharedMaterial = Manager.PlayerMaterials[_color - 2];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionPlayer = Vector3.zero;
        if (_color == 3)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                directionPlayer.y += 1;
            if (Input.GetKey(KeyCode.DownArrow))
                directionPlayer.y -= 1;
            if (Input.GetKey(KeyCode.LeftArrow))
                directionPlayer.x -= 1;
            if (Input.GetKey(KeyCode.RightArrow))
                directionPlayer.x += 1;
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
                directionPlayer.y += 1;
            if (Input.GetKey(KeyCode.S))
                directionPlayer.y -= 1;
            if (Input.GetKey(KeyCode.A))
                directionPlayer.x -= 1;
            if (Input.GetKey(KeyCode.D))
                directionPlayer.x += 1;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            GetComponent<Rigidbody>().AddExplosionForce(10000, transform.position, 10, 5);
        }
        
        transform.position += directionPlayer * (_moveSpeed * Time.deltaTime);

    }
    
    public void AddPoint()
    {
        _score++;
        OnScoreChanged?.Invoke();
    }
    private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            if(other.TryGetComponent<CellTrigger>(out CellTrigger trigger))
            {
                trigger.ChangeColor(_color);
                AddPoint();
            }
        }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
    }
}