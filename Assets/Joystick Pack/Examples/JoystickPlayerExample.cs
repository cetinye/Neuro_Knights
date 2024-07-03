using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody2D rb;

    public void Update()
    {
        Vector3 direction = Vector3.up * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        transform.position += speed * Time.deltaTime * direction.normalized;
    }
}