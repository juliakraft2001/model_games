using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Julia
{
    public class RotatingCylinder : MonoBehaviour
    {
        public float rotationSpeed = 15f;

        void Update()
        {
            // Rotates the object around the Y-axis in the opposite direction
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
