using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour {

    public Vector3 vel;
    public Vector3 look;
    public GameObject self;
    public Rigidbody rd;
    //PlayerController pc;
    public Vector3 curPos;
    CoolTimer jumpTimer;
    public float jumpForce = 110f;
    public bool jumpRdy = true;

    private void Awake()
    {
        self = gameObject;
        rd = GetComponent<Rigidbody>();
        //pc = GetComponent<PlayerController>();
        jumpTimer = GetComponent<CoolTimer>();
        //jumpTimer.TimerInit(2.0f);
    }

    // Update is called once per frame
    void Update () {


	}

    public void Move(Vector3 _Vec)
    {
        curPos = transform.position;
        curPos += transform.forward * _Vec.z;
        curPos += transform.right * _Vec.x;
        { // AddForce는 물리가 적용됨
            //Vector3 moveFor = transform.forward * _Vec.z;
            //moveFor += transform.right * _Vec.x;
            //rd.AddForce(moveFor);
        }
        rd.MovePosition(curPos);
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
