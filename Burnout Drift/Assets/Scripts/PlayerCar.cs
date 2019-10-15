using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour {

    [SerializeField] float acceleration = 8;
    [SerializeField] float turnSpeed = 5;
    Quaternion targetRotation;
    Rigidbody _rigidBody;

    float _sideSlipAmount = 0;
    Vector3 lastPosititon;

    public float SideSlipAmount
    {
        get
        {
            return _sideSlipAmount;
        }
    }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();  
    }
    void Update()
    {
        SetRotationPoint();
        SetSideSlip();
    }

    private void SetSideSlip()
    {
        Vector3 direction = transform.position - lastPosititon;
        Vector3 movement = transform.InverseTransformDirection(direction);
        lastPosititon = transform.position;
        _sideSlipAmount = movement.x
    }

    private void SetRotationPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector2.up, Vector3.zero);
        float distance; 
        if(plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, rotationAngle, 0);

        }
    }

    private void FixedUpdate()
    {
        float accelerationInput = acceleration * (Input.GetMouseButton(0) ? 1 : Input.GetMouseButton(1) ? -1 : 0) * Time.fixedDeltaTime;
        _rigidBody.AddRelativeForce(Vector3.forward * accelerationInput);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
    }
}
