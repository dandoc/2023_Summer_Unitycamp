using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public float rotSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(rotSpeed, 0, rotSpeed) * Time.deltaTime);
    }

}
