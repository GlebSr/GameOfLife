using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerColor = 2;
    [SerializeField] private float _moveSpeed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionPlayer = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
            directionPlayer.y += 1;
        if (Input.GetKey(KeyCode.DownArrow))
            directionPlayer.y -= 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            directionPlayer.x -= 1;
        if (Input.GetKey(KeyCode.RightArrow))
            directionPlayer.x += 1;
        //Vector3 moveDirX = new Vector3(Input.GetAxis("HorizontalAD"), Input.GetAxis("Vertical"), 0);

        if (Input.GetKeyDown(KeyCode.B))
        {
            GetComponent<Rigidbody>().AddExplosionForce(10000, transform.position, 10, 5);
        }
        
        transform.position += directionPlayer * (_moveSpeed * Time.deltaTime);

    }
    
    private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            if(other.TryGetComponent<CellTrigger>(out CellTrigger trigger))
            {
                trigger.ChangeColor(PlayerColor);
            }
        }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
    }
}
