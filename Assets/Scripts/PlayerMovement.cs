using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float runspeed;
    public float jumpForce;
    public float rotateSpeed;
    public float bulletCoolTime;
    public float turnSpeedMultiplier = 1.0f;

    public Rigidbody rigid;
    private bool isJump;
    private bool isWalk;
    public gameManager GM;


    public Transform weaponTransform;
    public GameObject bullet;

    public WeaponBehaviour weaponBehaviour;

    private Vector2 input;
    private Vector3 targetDir;
    private Camera mainCamera;


    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        runspeed = speed + speed / 2;
        rigid = GetComponent<Rigidbody>();
        isJump = false;
        isWalk = false;
        playerAnimator = GetComponent<Animator>();

        StartCoroutine(Fire(bulletCoolTime));

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        UpdateTargetDirection();

        if (input.x != 0 || input.y != 0)
        {

            

                if (targetDir.magnitude > 0.1f)
            {
                Vector3 lookDir = targetDir.normalized;
                Quaternion freeRotation = Quaternion.LookRotation(lookDir, transform.up);

                var differenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;

                var eulerY = transform.eulerAngles.y;

                if (differenceRotation < 0 || differenceRotation > 0) // 각 차이가 존재한다면
                {
                    eulerY = freeRotation.eulerAngles.y;
                }

                var euler = new Vector3(0, eulerY, 0);

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(euler), rotateSpeed * turnSpeedMultiplier);
            }
            //            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed);

            float moveSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isWalk = false;
                playerAnimator.SetBool("isRun", !isWalk);
                playerAnimator.SetBool("isWalk", isWalk);

                moveSpeed = runspeed;
            }

            else
            {
                isWalk = true;
                playerAnimator.SetBool("isWalk", isWalk);
                playerAnimator.SetBool("isRun", !isWalk);

                moveSpeed = speed;
                
            }
            rigid.AddForce(targetDir * moveSpeed, ForceMode.Impulse);
        }
        else if(input.x == 0 && input.y == 0)
        {

            isWalk = false;
            playerAnimator.SetBool("isWalk", isWalk);
            playerAnimator.SetBool("isRun", isWalk);

        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            playerAnimator.SetBool("isJump", isJump);
/*            isWalk = false;
            playerAnimator.SetBool("isWalk", isWalk);*/
            rigid.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);
        }


    }

    public void UpdateTargetDirection()
    {
        var forward = mainCamera.transform.TransformDirection(Vector3.forward);
        forward.y = 0;

        var right = mainCamera.transform.TransformDirection(Vector3.right);

        targetDir = input.x * right + input.y * forward;
    }

    private IEnumerator Fire(float coolTime)
    {
        while(true)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                Instantiate(bullet, weaponTransform.position, weaponTransform.rotation);
                weaponBehaviour.PlayWeaponSound();
                yield return new WaitForSeconds(coolTime);
            }
            else
            {
                yield return null;
            }
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

