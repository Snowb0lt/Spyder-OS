using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CongratsPicker : MonoBehaviour
{
    public UnityEvent PerfectRun, ItGotOut, DecentlyDone, CloseCall;
    public void whichCongrats()
    {
        float timeLeft = UIManager._instance.timeLeft;
        if (!UIManager._instance.isSpecimenOut)
        {
            PerfectRun.Invoke();
        }
        else
        {
            if (timeLeft <= 30 && timeLeft > 20)
            {
                ItGotOut.Invoke();
            }
            if (timeLeft >= 10 && timeLeft < 20)
            {
                DecentlyDone.Invoke();
            }
            if (timeLeft < 10)
            {
                CloseCall.Invoke();
            }
        }
        if (UIManager._instance.isSpecimenOut)
        {
            UIManager._instance.isSpecimenOut = false;
        }
        UIManager._instance.timeLeft = UIManager._instance.maxTime;
    }
}
