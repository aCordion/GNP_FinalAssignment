using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    const int MAX_POINT = 30;
    const int WIN_POINT = MAX_POINT / 2;
    const int MAX_PLAYERS = 8;
    public int[] players;
    //public int RedPoint;
    //public int BluePoint;

    private void Awake()
    {
        //RedPoint = 0;
        //BluePoint = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
