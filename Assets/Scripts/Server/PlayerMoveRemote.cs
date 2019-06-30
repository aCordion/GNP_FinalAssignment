using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveRemote : MonoBehaviour {

    public Vector3 vel;
    public Vector3 look;
    public GameObject self;
    public Rigidbody rd;
    //PlayerController pc;
    public Vector3 curPos;
    CoolTimer jumpTimer;
    public float jumpForce = 110f;
    public bool jumpRdy = true;

    Vector3 targetPos = new Vector3();
    float moveSpeed = 0f;

    private void Awake()
    {
        self = gameObject;
        rd = GetComponent<Rigidbody>();
        //pc = GetComponent<PlayerController>();
        jumpTimer = GetComponent<CoolTimer>();
        //jumpTimer.TimerInit(2.0f);
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.sqrMagnitude > 0.1f * 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed);
        }
    }

    public void Move(Vector3 _Vec)
    {
        targetPos = _Vec;
        
    }
    public void SetSpeed(float _speed)
    {
        moveSpeed = _speed;
    }

    public void Jump(float jump)
    {

        //점프 입력이 없을 경우 반환.
        if (jump == 0.0f)
        {
            return;
        }
        //점프가 준비된 경우 점프.
        else if (jumpRdy && jumpTimer.TimerDone())
        {
            rd.AddForce(0, jump * jumpForce, 0);
            jumpTimer.TimerReset();
        }

    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    jumpRdy = true;
    //}
    private void OnTriggerStay(Collider other)
    {
        jumpRdy = true;
    }
    private void OnTriggerExit(Collider other)
    {
        jumpRdy = false;
    }
}
