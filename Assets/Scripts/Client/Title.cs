using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

    Object pPrefab;
    GameObject player = null;

    private void Awake()
    {
        pPrefab = Resources.Load("Prefabs/Player");
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 200, 70), "KeyBoard");
        if (GUI.Button(new Rect(30, 35, 70, 30), "Enter") || Input.GetKey(KeyCode.Return))
        {
            GameObject.FindGameObjectWithTag("Network").GetComponent<ClientNetwork>().ConnectToServer();
            if (player == null)
            {
                Instantiate(pPrefab);
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (GUI.Button(new Rect(120, 35, 70, 30), "Escape") || Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }

}
