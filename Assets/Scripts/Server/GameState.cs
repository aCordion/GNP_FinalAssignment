using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    enum GameStates
    {
        Ready,
        Start,
        Playing,
        End,
        Reset
    }

    [SerializeField]
    GameStates curState = GameStates.Ready;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}

}
