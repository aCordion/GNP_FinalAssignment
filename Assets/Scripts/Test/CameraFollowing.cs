using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour {

    public Camera self;
    public GameObject player;

    private void Awake()
    {
        //self = Camera.main;
        self = gameObject.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        self.transform.position = player.transform.position;
        //Vector3 temp = new Vector3(0.0f, 2.0f, -2.0f) + player.transform.position;
        //Vector3 temp = new Vector3(0.0f, 6.5f, 0.0f) + player.transform.position;
        //self.transform.rotation.SetFromToRotation(self.transform.position, player.transform.position);
        self.transform.rotation = Quaternion.Euler(0.0f, 0, 0);
        self.transform.SetParent(player.transform);
        self.transform.position.Set(0.0f, 6.5f, 0.3f);// = new Vector3(0.0f, 6.5f, 0.0f);
    }
    private void LateUpdate()
    {
        //self.transform.LookAt(player.transform);
    }

    //public void LookUpdate(Ray _ray, float _rayDist)
    public void LookUpdate(Vector3 _point)
    {
        //_point.x = 0;
        //_point.y = 0;
        //_point.z = 0;
        //gameObject.transform.LookAt(_point);
    }
}
