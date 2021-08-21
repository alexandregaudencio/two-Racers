using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{

    [SerializeField] private float maxTime;
    float currentTime;
    [SerializeField] private Text UIStartTime;

    private void Awake()
    {
        currentTime = maxTime;
    }

    public bool IsCountdownOver()
    {
        return (currentTime > 0.0f) ? false : true;
    }

    private void FixedUpdate()
    {
        if (!IsCountdownOver())
        {
            currentTime -= Time.fixedDeltaTime;
            
            string time = string.Format("{0:0}", currentTime);
            UIStartTime.text = time;
        }
        else
        {         
            UIStartTime.text = "Go!";

        }
        

    }


}
