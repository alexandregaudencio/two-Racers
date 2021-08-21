using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    public List<GameObject> checkPointsObjects;
    public int[] checkPointPassed;
    public GameObject finishPoint;


    void Start()
    {
        //Debug.Log(checkPointPassed.GetValue(1));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CheckStep(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CheckStep(3);

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
        }

    }

    private void CheckStep(int step)
    {
        //if(checkPointPassed.IndexOf(checkPointPassed.Length-1) == step)
        {
            checkPointPassed.SetValue(step, checkPointPassed.Length);
        }
    }


    public void OnFinishPoint(GameObject stepObject)
    {
       
       int indexStep = checkPointsObjects.IndexOf(stepObject);
        Debug.Log(indexStep);
       //if(checkPointPassed.IndexOf(checkPointPassed.Length-1) == indexStep)
        {
            //checkPointPassed.Add(step);
        }
    }



}
