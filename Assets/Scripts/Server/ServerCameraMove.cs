using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCameraMove : MonoBehaviour {

    public float cameraXRot = 0f;
    public float curCameraXRot = 0;
    public float cameraYRot = 0f;
    public float curCameraYRot = 0;

    // Update is called once per frame
    void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float yR = Input.GetAxis("Mouse X");
        float xR = Input.GetAxis("Mouse Y");

        //float up = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;
        //float down = Input.GetKey(KeyCode.C) ? -1.0f : 0.0f;
        float y = 0;
        if (Input.GetKey(KeyCode.Space))
        {
            y = 1.0f;
        }
        else if (Input.GetKey(KeyCode.C))
        {
            y = -1.0f;
        }
        else
        {
            y = 0;
        }

        Vector3 vec = new Vector3(h, y, v) * 0.05f;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            vec = new Vector3(h, y, v) * (0.05f * 2.0f);
        }
        Move(vec);
        Rotate(yR, xR);
    }
    
    void Move(Vector3 _Vec)
    {
        Vector3 curPos;
        curPos = transform.position;
        curPos += transform.forward * _Vec.z;
        curPos += transform.up * _Vec.y;
        curPos += transform.right * _Vec.x;
        transform.position = curPos;
    }

    void Rotate(float _yR, float _xR)
    {
        //rd.MoveRotation(rd.rotation * Quaternion.Euler(new Vector3(0f, _yR, 0f) * 5.0f));
        

        cameraXRot = _xR * 4.0f;
        curCameraXRot -= cameraXRot;
        curCameraXRot = Mathf.Clamp(curCameraXRot, -80f, 80f);

        cameraYRot = _yR * 5.0f;
        curCameraYRot -= cameraYRot;
        //curCameraYRot = Mathf.Clamp(curCameraYRot, -80f, 80f);

        //pSight.transform.localEulerAngles = new Vector3(curCameraXRot, 0f, 0f);
        Camera.main.transform.localEulerAngles = new Vector3(curCameraXRot, -curCameraYRot, 0f);
    }
}
