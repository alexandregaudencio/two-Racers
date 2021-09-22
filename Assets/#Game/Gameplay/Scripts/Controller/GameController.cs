using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Text UIScore;
    [SerializeField] private Text UILap;
    [SerializeField] private GameObject UIFinishImage;

    private bool isGameStarted;
    private TimerCountdown timerCountdown;
    public int score = 0;
    public int chegou = 0;
    private int lap = 0;
    [SerializeField] private int maxLap;

    public Text currentScore;
    public Text score1;
    public Text score2;
    public Text maxScore;
    private void Start()
    {
        Cliente.instance.pegarNomeJogadores();
        instance = this;
        timerCountdown = GetComponent<TimerCountdown>();
        ChangeUIScore(0);
        CheckLap();
        //playerCar.transform.position = spawnPoints[0].transform.position;
    }

    private void FixedUpdate()
    {
        CheckingStartGame();
       
        if (chegou == 1)
        {
            FinishGame();
            chegou = 2;
        }
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
            Cliente.instance.contadorFinish();
            Cliente.instance.finishGame();
            //FinishGame();
        }

    }

    private void FinishGame()
    {
        UIFinishImage.gameObject.SetActive(true);
        Cliente.instance.pegarPontoMax();
        Cliente.instance.score1= Cliente.instance.pegarScoreJogadores(Cliente.instance.jogador1);
        Cliente.instance.score2=Cliente.instance.pegarScoreJogadores(Cliente.instance.jogador2);
        GameController.instance.score1.text = ("Pontuacao jogador1: " + Cliente.instance.score1.ToString());
        GameController.instance.score2.text = ("Pontuacao jogador2: " + Cliente.instance.score2.ToString());
        // Cliente.instance.enviarScore2();
        //Cliente.instance.enviarScore1();
    }

}
