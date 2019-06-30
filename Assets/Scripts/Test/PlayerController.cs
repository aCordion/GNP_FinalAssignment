using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
public class PlayerController: MonoBehaviour {

    public GameObject player;
    public PlayerMove pMover;
    public Camera pSight;
    public float cameraRot = 0f;
    public float curCameraRot = 0;
    //public Ray ray;
    //public float rayDist;
    public Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    public BulletFire fire;
    public bool canFire = true;
    public Rigidbody rd;

    public float targetDelay, curDelay;
    public bool delayDone = false;
    public bool isClient = false;

    public float moveSpeed = 0.05f;

    private void Awake()
    {
        player = gameObject;
        //pSight = Camera.main;
        pSight = GetComponentInChildren<Camera>();
        pMover = GetComponent<PlayerMove>();
        rd = GetComponent<Rigidbody>();
        fire = transform.GetComponentInChildren<BulletFire>();
    }

    private void Update()
    {
        if (isClient) return;

        int f1 = (int)Input.GetAxisRaw("Fire1");

        
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

    // Update is called once per frame
    void FixedUpdate () {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 vec = new Vector3(h, 0, v) * moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            vec = new Vector3(h, 0, v) * (moveSpeed * 2.0f);
        }
        pMover.Move(vec);

        float j = Input.GetAxis("Jump");
        pMover.Jump(j);

        float yR = Input.GetAxis("Mouse X");
        float xR = Input.GetAxis("Mouse Y");

        rd.MoveRotation(rd.rotation * Quaternion.Euler(new Vector3(0f, yR, 0f) * 5.0f));
        //pSight.transform.rotation = Quaternion.Euler(new Vector3(-xR, 0f, 0f) * 4.0f);
        //pSight.transform.localEulerAngles = new Vector3(Mathf.Clamp(xR * 4.0f, -80f, 80f), 0f, 0f);

        cameraRot = xR * 4.0f;
        curCameraRot -= cameraRot;
        curCameraRot = Mathf.Clamp(curCameraRot, -80f, 80f);
        pSight.transform.localEulerAngles = new Vector3(curCameraRot, 0f, 0f);

        //ray = pSight.ScreenPointToRay(Input.mousePosition);

        //if (groundPlane.Raycast(ray, out rayDist))
        //{
        //    Vector3 point = ray.GetPoint(rayDist);
        //    Debug.DrawLine(ray.origin, point, Color.cyan);
        //    pMover.LookPos(point);
        //    pSight.gameObject.GetComponent<CameraFollowing>().LookUpdate(point);
        //}
    }
   
}
