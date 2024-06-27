using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dvigenie : MonoBehaviour
{
    public float gravity = 9.8f;
    public float maxSpeed = 10f;
    public float uskorenie = 0.6f;
    public float tormoz = 3.6f;

    private float speed = 0;
    private bool dvigA = false;
    private bool dvigD = false;
    private float _fallVelocity = 0;
    private Vector3 _moveVector;
    private CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        MoveventUpdate();
    }

    private void Tormoz(bool dvig)
    {
        while (speed > 0)
        {
            speed -= tormoz * Time.deltaTime;
        }
        if(speed < 0) { speed = 0; dvig = false; }
        if (speed == 0) { dvig = false; }
    }

    private void MoveventUpdate()
    {
        _moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
        {
            _moveVector += transform.right;
            if (speed < maxSpeed)
            {
                speed += uskorenie * Time.deltaTime;    
            }
            dvigD = true;
        }
        if(Input.GetKeyUp(KeyCode.D) && dvigD == true)
        {
            Tormoz(dvigD);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _moveVector -= transform.right;
            if (speed < maxSpeed)
            {
                speed += uskorenie * Time.deltaTime;
                dvigA = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.A) && dvigA == true)
        {
            Tormoz(dvigA);
        }
    }


    void FixedUpdate()
    {
        _characterController.Move(_moveVector * speed * Time.fixedDeltaTime);

        _fallVelocity += gravity * Time.fixedDeltaTime;
        _characterController.Move(Vector3.down * _fallVelocity * Time.fixedDeltaTime);

        if (_characterController.isGrounded) _fallVelocity = 0;
    }
}
