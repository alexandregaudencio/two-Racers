using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text UIScore;
    [SerializeField] private Text UILap;
    [SerializeField] private Image UIFinishImage;
    private int score = 0;
    private int lap = 0;
    [SerializeField] private int maxLap;

    private bool isGameStarted;
    private TimerCountdown timerCountdown;


    private void Start()
    {
        timerCountdown = GetComponent<TimerCountdown>();
        ChangeUIScore(0);
        CheckLap();
    }
    private void Update()
    {
        isGameStarted = timerCountdown.IsCountdownOver();
        //TODO: fazer para múltiplos playerscontrollers
        GameObject.FindObjectOfType<PlayerController>().enabled = isGameStarted;

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
