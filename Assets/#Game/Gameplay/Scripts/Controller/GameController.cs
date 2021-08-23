﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text UIScore;
    [SerializeField] private Text UILap;
    [SerializeField] private Image UIFinishImage;

    private bool isGameStarted;
    private TimerCountdown timerCountdown;
    private int score = 0;
    private int lap = 0;
    [SerializeField] private int maxLap;

    private void Start()
    {
        timerCountdown = GetComponent<TimerCountdown>();
        ChangeUIScore(0);
        CheckLap();
        //playerCar.transform.position = spawnPoints[0].transform.position;
    }

    private void FixedUpdate()
    {
        CheckingStartGame();

    }



    private void CheckingStartGame()
    {
        if (isGameStarted) return;
        
        PlayerController[] playerController = FindObjectsOfType<PlayerController>();
        foreach(PlayerController controller in playerController) {
            controller.enabled = !timerCountdown.IsTimerRunning() ;

        }
        isGameStarted = !timerCountdown.IsTimerRunning();
    }


    public void ChangeUIScore(int score)
    {
        this.score += score;
        UIScore.text = "Pontos: "+this.score.ToString();
    }

    public void CheckLap()
    {
        if (lap < maxLap)
        {
            lap++;
            UILap.text = "Volta: " + this.lap.ToString() + "/" + maxLap.ToString();
        }
        else
        {
            UILap.text = "Partida acabou! ";
            FinishGame();
        }

    }

    private void FinishGame()
    {
        UIFinishImage.gameObject.SetActive(true);

    }

}
