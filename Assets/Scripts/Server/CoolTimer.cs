using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTimer : MonoBehaviour{

    public float from = 0.0f;
    public float to = 1.0f;
    public bool delayDone = true;

    //CoolTimer(float To, bool Start = false)
    //{
    //    to = To;
    //    if (Start)
    //    {
    //        TimerReset();
    //    }
    //}

    private void FixedUpdate()
    {
        switch (delayDone)
        {
            case false:
                from += (1.0f * Time.fixedDeltaTime);
                if(from >= to)
                {
                    from = 0.0f;
                    delayDone = true;
                }
                break;

            default:
                break;
        }
    }

    public void TimerInit(float To)
    {
        to = To;
    }

    public void TimerReset()
    {
        delayDone = false;
    }

    public bool TimerDone()
    {
        return delayDone;
    }

}
