using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{

    [SerializeField] private float maxTime;
    float currentTime;
    [SerializeField] private GameObject UIStartTime;
    string UIStartTimeText = "0";
    private void Awake()
    {
        currentTime = maxTime;
    }

    public bool IsTimerRunning()
    {
        return (currentTime > 0.000f);
    }

    private void FixedUpdate()
    {
        if (IsTimerRunning())
        {
            currentTime -= Time.fixedDeltaTime;
            
            UIStartTimeText = string.Format("{0:0}", currentTime);
            UIStartTime.gameObject.GetComponent<Text>().text = UIStartTimeText;

        }
        else
        {         
            StartCoroutine(AnimationUIStartTime());
        }
        

    }

    private IEnumerator AnimationUIStartTime()
    {
        UIStartTimeText = "Go!";
        yield return new WaitForSeconds(2f);
        UIStartTime.SetActive(false);
        
    }

}
