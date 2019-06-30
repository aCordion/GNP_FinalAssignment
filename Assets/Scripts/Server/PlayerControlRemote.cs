using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlRemote : MonoBehaviour {

    public GameObject player;
    public PlayerMoveRemote pMover;
    public Camera pSight;
    public float cameraRot = 0f;
    public float curCameraRot = 0;

    public BulletFireRemote fire;
    public bool canFire = true;
    public Rigidbody rd;
    public PlayerMoveRemote pmr;

    public float targetDelay, curDelay;
    public bool delayDone = false;
    public bool isClient = false;

    TransformData target = new TransformData();

    public float moveSpeed = 0.05f;

    private void Awake()
    {
        player = gameObject;
        //pSight = Camera.main;
        pSight = GetComponentInChildren<Camera>();
        pMover = GetComponent<PlayerMoveRemote>();
        rd = GetComponent<Rigidbody>();
        pmr = transform.GetComponentInChildren<PlayerMoveRemote>();
        fire = transform.GetComponentInChildren<BulletFireRemote>();
    }

    private void Update()
    {
        if (isClient) return;

        int f1 = target.shot ? 1 : 0;

        //int f1 = (int)Input.GetAxisRaw("Fire1");

        switch (f1)
        {
            case 0:
                canFire = true;
                break;
            case 1:
                if (canFire)
                {
                    fire.Fire();
                    canFire = false;
                }
                break;
            default:
                break;
        }

    }
    private void Start()
    {
        pMover.SetSpeed(moveSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //Vector3 vec = new Vector3(h, 0, v) * moveSpeed;
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    vec = new Vector3(h, 0, v) * (moveSpeed * 2.0f);
        //}        
        Vector3 pos = new Vector3(target.posX, target.posY, target.posZ);
        Vector3 rdRot = new Vector3(0.0f, target.rotY, 0.0f);
        Vector3 camRot= new Vector3(target.camRotX, 0.0f, 0.0f);
        float j = target.jump ? 1.0f : 0.0f;
        //float yR = Input.GetAxis("Mouse X");
        //float xR = Input.GetAxis("Mouse Y");


        pMover.Move(pos);        
        pMover.Jump(j);

        //rd.MoveRotation(rd.rotation * Quaternion.Euler(new Vector3(0f, yR, 0f) * 5.0f));
        rd.rotation = Quaternion.Euler(rdRot);
        pSight.transform.rotation = Quaternion.Euler(camRot);

    }

    public void SetTransforms(TransformData td)
    {
        //Vector3 pos = new Vector3(td.posX, td.posY, td.posZ);
        //Vector3 rot = new Vector3(td.camRotX, td.rotY, td.rotZ);
        target = td;
    }

}
