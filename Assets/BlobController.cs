using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobController : MonoBehaviour
{
    public float forwardSpeed = 2.5f;
    public float rotationSpeed = 2.0f;
    public GameObject directionController;
    public float deadband = 0.1f;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // match the controller target angle
        var targetAngle = directionController.transform.eulerAngles.y;

        // Smoothly change rotation to match target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), rotationSpeed * Time.deltaTime); ;
        Vector3 Direction = transform.rotation * Vector3.forward;

        // Controller distance from body determines speed of moevement forward
        var position = new Vector3(transform.position.x, 1, transform.position.z);
        var controllerPosition = new Vector3(directionController.transform.position.x, 1, directionController.transform.position.z);
        // todo: clip at max distance
        distance = Vector3.Distance(position, controllerPosition);

        // deadband
        if (Math.Abs(distance) > deadband)
        {
            transform.position += (Direction * Time.deltaTime * ((distance-deadband) * forwardSpeed));
        }
    }
}
