using System.Collections;
using UnityEngine;

public class LetterSpin : MonoBehaviour
{
    [SerializeField] private float rotationSpeedX = 0f;
    [SerializeField] private float rotationSpeedY = 50f;
    [SerializeField] private float rotationSpeedZ = 0f;

    void Update()
    {
        // Rotate the object around its local axes
        transform.Rotate(new Vector3(rotationSpeedX, rotationSpeedY, rotationSpeedZ) * Time.deltaTime);
    }
}
