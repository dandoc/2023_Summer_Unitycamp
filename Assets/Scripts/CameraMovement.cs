using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;

    private void Awake()
    {
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
    }
}
