using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Julia
{
public class RotatingCylinder : MonoBehaviour
{
    public float rotationSpeed = 20f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
}