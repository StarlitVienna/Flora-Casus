using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zarabatanaScript : MonoBehaviour
{
    private Transform zarabatanaTransform;
    public seeds bulletPrefab;

    void Start()
    {
        zarabatanaTransform = gameObject.GetComponent<Transform>();
    }

    
    void Update()
    {
        
    }

    public void shoot(float aimAngle, Vector2 aimAngleXY)
    {
        Instantiate(bulletPrefab, zarabatanaTransform.position, Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg));
        //Debug.Log(aimAngleQuaternion);
    }
}
