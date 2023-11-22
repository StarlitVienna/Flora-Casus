using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PivotDirection : MonoBehaviour
{
    private Transform pivotTransform;
    private Vector3 pivotNewPos;
    [SerializeField] float rotationX;
    public PlayerController playerController;
    Quaternion RotationVector;
    private void Start()
    {
        pivotTransform = gameObject.GetComponent<Transform>();
        //playerController = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        //rotationX = playerController.movementInput.x*180;
        //pivotTransform.rotation.SetEulerRotation(69*rotationX, 0, 0);
        //RotationVector.x = playerController.att
        //RotationVector.y = playerController.movementInput.x*180;

        // do an if case so it will only trigger the switch when attacking

        if (playerController.attacking)
        {
            switch (playerController.attackDirection)
            {
                case PlayerController.attackDirections.right:
                    RotationVector.x = 0;
                    RotationVector.y = 0;
                    pivotNewPos.x = -0f;
                    pivotNewPos.y = 0f;
                    pivotTransform.position = pivotNewPos + playerController.playerTransform.position;
                    break;
                case PlayerController.attackDirections.left:
                    RotationVector.y = 180;
                    RotationVector.x = 0;
                    pivotNewPos.x = 0f;
                    pivotNewPos.y = 0f;
                    pivotTransform.position = pivotNewPos + playerController.playerTransform.position;
                    break;
                case PlayerController.attackDirections.up:
                    RotationVector.x = 0;
                    RotationVector.y = 0;
                    pivotNewPos.x = -0.25f;
                    pivotNewPos.y = 0.6f;
                    pivotTransform.position = pivotNewPos + playerController.playerTransform.position;
                    break;
                case PlayerController.attackDirections.down:
                    RotationVector.x = 0;
                    RotationVector.y = 0;
                    pivotNewPos.x = -0.2f;
                    pivotNewPos.y = -0.2f;
                    pivotTransform.position = pivotNewPos + playerController.playerTransform.position;
                    break;

            }
            RotationVector.z = 0;
            //pivotTransform.Rotate(RotationVector);
            pivotTransform.SetPositionAndRotation(pivotTransform.position, RotationVector);
            //Debug.Log(playerController.movementInput);
        }
    }
}
