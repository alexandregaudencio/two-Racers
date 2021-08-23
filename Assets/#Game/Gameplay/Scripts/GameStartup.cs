using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    [SerializeField] private GameObject[] carPlayers;
    [SerializeField] private Transform[] spawnPoints;


    void Start()
    {
        //carPlayers[1].SetActive(false);
        SetCarPosition();

    }

    void SetCarPosition()
    {
        carPlayers[0].transform.position = spawnPoints[0].transform.position;
        carPlayers[1].transform.position = spawnPoints[1].transform.position;
    }


}
