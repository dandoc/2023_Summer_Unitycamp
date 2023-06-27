using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public Rigidbody rigid;
    private bool isJump;
    private bool isWalk;
    public gameManager GM;

    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        isJump = false;
        isWalk = false;
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        rigid.AddForce(dir * speed, ForceMode.Impulse);

        if (h != 0 || v != 0)
        {
            isWalk = true;
            playerAnimator.SetBool("isWalk", isWalk);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);

        }
        else if(h == 0 && v == 0)
        {
            isWalk = false;
            playerAnimator.SetBool("isWalk", isWalk);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            playerAnimator.SetBool("isJump", isJump);
            isWalk = false;
            playerAnimator.SetBool("isWalk", isWalk);
            rigid.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);
        }

        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
            isWalk = false;
            playerAnimator.SetBool("isWalk", isWalk);
            playerAnimator.SetBool("isJump", isJump);
        }
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
            GM.GetItem(--GM.itemCount);
            GM.MoveNextStage();
        }
    }
}

