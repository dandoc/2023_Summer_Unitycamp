using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float bulletSpeed;

    public Rigidbody bulletRigid;

    // Update is called once per frame
    void Awake()
    {
        bulletRigid = GetComponent<Rigidbody>();
        bulletRigid.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(this.gameObject, 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Destroy(collision.gameObject, 2);


            Destroy(this.gameObject);
        }
    }
}
