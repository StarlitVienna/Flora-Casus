using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Vector2 dashVector;

    private void Start()
    {
        dashVector = new Vector2(40, 40);
    }

    public Vector2 dashAction(Vector2 movementInput)
    {
        return (dashVector * movementInput);
    }
}
