using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public CheckPoint[] checkPoints;
    GameController GameController;

    private void Awake()
    {
        checkPoints = FindObjectsOfType<CheckPoint>();
        GameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            CheckCheckpoints();
        }
    }


    void CheckCheckpoints()
    {
        foreach(CheckPoint cp in checkPoints)
        {
            if(!cp.checkedPoint)
            {
                Debug.Log("Não deu a volta corretamente!");
                ResetCheckPoints();
                return;
            }
        }

        GameController.CheckLap();

    }

    void ResetCheckPoints()
    {
        foreach (CheckPoint checkpoint in checkPoints)
        {
            checkpoint.checkedPoint = false;
        }
    }
}

