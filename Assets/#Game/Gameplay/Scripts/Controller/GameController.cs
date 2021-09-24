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
    [SerializeField] private GameObject vencedor1Ui;
    [SerializeField] private GameObject vencedor2Ui;
    [SerializeField] private GameObject empateUi;
    [SerializeField] private GameObject jogador1Ui;
    [SerializeField] private GameObject jogador2Ui;

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
        Cliente.instance.startingPlayers=false;
        instance = this;
        timerCountdown = GetComponent<TimerCountdown>();
        ChangeUIScore(0);
        CheckLap();
        if (Cliente.instance.id % 2 == 1)
        {
            jogador1Ui.SetActive(true);
        }
        if (Cliente.instance.id % 2 == 0)
        {
            jogador2Ui.SetActive(true);
        }
        //playerCar.transform.position = spawnPoints[0].transform.position;
    }

    private void FixedUpdate()
    {
        CheckingStartGame();
       
        if (chegou == 1)
        {
            FinishGame();
            //chegou = 2;
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
            if (Cliente.instance.id % 2 == 1)
            {
                Cliente.instance.vencedor = 1;
            }
            if (Cliente.instance.id % 2 == 0)
            {
                Cliente.instance.vencedor = 2;
            }

        }

    }

    private void FinishGame()
    {
        //chegou = 2;
        UIFinishImage.gameObject.SetActive(true);
        Cliente.instance.pegarPontoMax();
        Cliente.instance.pegarScoreJogador1();
        Cliente.instance.pegarScoreJogador2();
      /*  if (Cliente.instance.vencedor == 1)
        {
            Cliente.instance.score1 = Cliente.instance.score1 + 5;
        }
        if (Cliente.instance.vencedor == 2)
        {
            Cliente.instance.score2 = Cliente.instance.score2 + 5;
        }*/
        if (Cliente.instance.score1 > Cliente.instance.score2)
        {
            vencedor1Ui.SetActive(true);
        }
        if (Cliente.instance.score2 > Cliente.instance.score1)
        {
            vencedor2Ui.SetActive(true);
        }
        if (Cliente.instance.score2== Cliente.instance.score1)
        {
            //empateUi.SetActive(true);
        }
        GameController.instance.score1.text = ("Pontuacao jogador1: " + Cliente.instance.score1.ToString());
        GameController.instance.score2.text = ("Pontuacao jogador2: " + Cliente.instance.score2.ToString());
    }

}
