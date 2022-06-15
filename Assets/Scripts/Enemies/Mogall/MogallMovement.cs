using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MogallMovement : MonoBehaviour
{
    [SerializeField]
    private bool isRotationTowardsLeft;
    [SerializeField]
    private float rotationSpeed;


    private bool isRotatingToLeft;
    private float baseRotation;

    private void Start()
    {
        baseRotation = transform.localEulerAngles.z;
        isRotatingToLeft = false;
    }

    public void Patrol()
    {
        Rotate();
    }

    // Rotates object to its left or the right
    private void Rotate()
    {
        // Rotation of object is towards its left
        if(isRotationTowardsLeft)
        {
            if(isRotatingToLeft)
            {
                transform.localEulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;

                if (transform.localEulerAngles.z >= baseRotation + 89f)
                    isRotatingToLeft = false;
            }
            else
            {
                transform.localEulerAngles -= Vector3.forward * rotationSpeed * Time.deltaTime;

                if (transform.localEulerAngles.z <= baseRotation)
                    isRotatingToLeft = true;
            }      
        }

        // Rotation of object is towards its right
        else
        {
            if (isRotatingToLeft)
            {
                transform.localEulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;

                if (transform.localEulerAngles.z >= baseRotation)
                    isRotatingToLeft = false;
            }
            else
            {
                transform.localEulerAngles -= Vector3.forward * rotationSpeed * Time.deltaTime;

                if (transform.localEulerAngles.z <= baseRotation - 89f)
                    isRotatingToLeft = true;
            }
        }      
    }
}
