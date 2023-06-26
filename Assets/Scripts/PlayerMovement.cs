using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public Rigidbody rigid;
    private bool isJump;
    public gameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        isJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            rigid.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);
            isJump = true;
        }

        rigid.AddForce(new Vector3(h, 0, v) * speed, ForceMode.Impulse);  
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            isJump = false;
        /*if (collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
            score += 10;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            GM.GetItem(++GM.itemCount);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Finish")
        {
            GM.MoveNextStage();
        }
        if (other.gameObject.tag == "void")
        {
            GM.MoveNextStage();
        }
    }
}

