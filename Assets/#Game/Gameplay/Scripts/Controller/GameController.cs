using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text UIScore;
    [SerializeField] private Text UILap;
    private int score = 0;
    private int lap = 0;
    [SerializeField] private int maxLap;


    private void Start()
    {
        ChangeUIScore(0);
    }

    public void ChangeUIScore(int score)
    {
        this.score = score;
        UIScore.text = "Pontos: "+this.score.ToString();

    }

    public void CheckLap()
    {
        if (lap < maxLap)
        {
            lap++;
            UILap.text = "Vola: " + this.lap.ToString() + "/" + maxLap.ToString();

        }
        else
        {
            UILap.text = "Partida acabou! ";
        }

    }



}
