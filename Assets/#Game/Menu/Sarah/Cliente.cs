using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System;
enum Estado
{
    VALIDADO,
    INVALIDADO,
    SUCESSO,
    SUCESSOCOMERRO,
    NAOFOIACHADO,
    ERRO,
    FOI,
    NAOFOI
}
public class Cliente : MonoBehaviour
{
    private bool gameplay=false;
    public static Cliente instance;
    string usuario;
    public string usuarioLogado;
    public string jogador1, jogador2;
    int senha;
    public int vencedor = 0;
    int pontos;
    public int score1;
    public  int score2;
    int result,result2;
    public string[] point;
    public string score;
    public int i;
    private float x, y, z;
    private TcpClient cliente;
    private StreamReader reader = null;
    private StreamWriter writer = null;
    private Thread thread;
    private bool running = false;
    public bool startingPlayers = false;
    System.Random rnd;
    int maxScore;
    int currentScore;
    public int id;
    public int cod;
    public int a;
    public int loginCheck = 0;
    public int cadastroCheck = 0;
    public int maxScoreCheck = 0;
    private bool pontuarCheck = false;
    private bool rankCheck = false;
    public string[] rankArray;
    private bool posicaoCheck = false;
    private bool posicao2Check = false;
    void Start()
    {
        if (instance == null)
        {
            rnd = new System.Random();

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void Awake()
    {
        Desconectar();
    }


    void Update()
    {
        finishGame();
        enviarPosicao1();
        enviarPosicao2();

        if (SceneManager.GetActiveScene().name.Equals("Gameplay"))
        {
            gameplay = true;
        }
        if (startingPlayers)
        {
            start2();
        }
        loginR();
        cadastroR();
        pontuarR();
        masxScoreR();
        rankR();
        posicaoR();
        posicao2R();
        Debug.Log("i: " + i);

       // a = GameController.instance.chegou;
    }
    public void cadastroR()
    {
        if (cadastroCheck == 1)
        {
            InputController.instance.msg = "realizado com sucesso";
            MenuController.instance.cadastrarCerto();
            cadastroCheck = 0;
        }
        if (cadastroCheck == 2)
        {
            InputController.instance.msg = "Cadastro já existente, tente outro";
            InputController.instance.usuarioInput.text = "";
            InputController.instance.senhaInput.text = "";
            cadastroCheck = 0;
        }
    }
    public void masxScoreR()
    {
        if (maxScoreCheck == 1)
        {
            Debug.Log("Pontuacao maxima atualizada");
            inserirPonto();
            GameController.instance.currentScore.text = ("Sua pontuacao: " + currentScore.ToString());
            pegarPontoMax();
            GameController.instance.maxScore.text = ("Novo Recorde: " + maxScore.ToString());
            maxScoreCheck = 0;
        }
        if (maxScoreCheck == 2)
        {
            Debug.Log("Pontuacao maxima nao atualizada");
            GameController.instance.currentScore.text = ("Sua pontuacao: " + currentScore.ToString());
            GameController.instance.maxScore.text = ("Seu Recorde: " + maxScore.ToString());
            maxScoreCheck = 0;
        }
       
    }
    public void loginR()
    {
        if (loginCheck == 1)
        {
            MenuController.instance.logarCerto();
            usuarioLogado = usuario;
            InputController.instance.msg = ("Login válido: " + usuarioLogado);
            loginCheck = 0;
        }
        if (loginCheck == 2)
        {
            InputController.instance.msg = "Nao foi achado o usuario";
            InputController.instance.usuarioLInput.text = "";
            InputController.instance.senhaLInput.text = "";
            loginCheck = 0;
        }
        if (loginCheck == 3)
        {
            InputController.instance.msg = "senha incorreta";
            InputController.instance.senhaLInput.text = "";
            loginCheck = 0;
        }
    }
    public void pontuarR()
    {
        if (pontuarCheck == true)
        {
            GameController.instance.UIScore.text = ("Pontos :" + score);
            Debug.Log("score: " + score);
            pontuarCheck = false;
        }
        
    }
    public void rankR()
    {
        if (rankCheck == true)
        {
            for (int i = 0; i < 7; i++)
            {
                InputController.instance.ranks[i].text = rankArray[i];
            }
            rankCheck = false;
        }

    }
    public void posicaoR()
    {
        if (posicaoCheck == true)
        {
            PlayerController.instance.trans.position = new Vector3(x, y, 0);
           PlayerController.instance.carRigidbody.MoveRotation(z);
            Debug.Log("position" + PlayerController.instance.trans.position);

            posicaoCheck = false;
        }

    }
    public void posicao2R()
    {
        if (posicao2Check == true)
        {
            PlayerController2.instance.trans.position = new Vector3(x, y, 0);
            PlayerController2.instance.carRigidbody.MoveRotation(z);
            Debug.Log("position" + PlayerController2.instance.trans.position);

            posicao2Check = false;
        }

    }

    private void Run()
    {
        cliente = new TcpClient("127.0.0.1", 5000);
        NetworkStream stream = cliente.GetStream();

        reader = new StreamReader(stream);
        writer = new StreamWriter(stream);
        string dados;
       
        
        while (running)
        {
            try
            {
               
                dados = reader.ReadLine();
                string[] info = dados.Split(';');
                Debug.Log(info[0]);
                //finishGame();
                //cadastrar
                if (info[0].Equals("c"))
                {
                    if (info[1].Equals(Estado.VALIDADO.ToString()))
                    {
                        cadastroCheck = 1;
                    }
                    else
                    {
                        cadastroCheck = 2;
                    }
                }
                //logar
                if (info[0].Equals("l"))
                {
                   
                    if (info[1].Equals(Estado.SUCESSO.ToString()))
                    {
                        loginCheck = 1;
                    }
                    if (info[1].Equals(Estado.NAOFOIACHADO.ToString()))
                    {
                        loginCheck = 2;
                    }
                    if (info[1].Equals(Estado.SUCESSOCOMERRO.ToString()))
                    {
                        loginCheck = 3;
                    }
                }
                //ddesconectar n precisa
                //pontuar n precisa
                //inserir obs n precisa
                //inserir col n precisa
                //pegar ponto
                if (info[0].Equals("s"))
                {
                    score = info[1];
                    Debug.Log("score: "+score);
                    pontuarCheck = true;
                }
                //enviar score1
                if (info[0].Equals("y"))
                {
                    int.TryParse(info[1], out result);
                   
                        score1 = result;
                   
                    
                }
                //enviar score2
                if (info[0].Equals("u"))
                {
                    int.TryParse(info[1], out result2);
                   
                        score2 = result2;
                   
                   
                }
                //finishgame
                if (info[0].Equals("b"))
                {
                    int value;
                    int.TryParse(info[1], out value);
                    GameController.instance.chegou = value;
                }
                //contadorfinal n precisa
                //pegar id
                if (info[0].Equals("z"))
                {
                    int.TryParse(info[1], out id);
                    if (id % 2 == 1) cod = 1;
                    if (id % 2 == 0) cod = 2;
                }
                //pegar nomes jogadores
                if (info[0].Equals("0"))
                {
                    
                    string[] info2 = info[1].Split('.');
                    jogador1 = info2[0];
                    jogador2 = info2[1];
                    Debug.Log("jogador1: " + jogador1);
                    Debug.Log("jogador2: " + jogador2);

                }
                //score dos jogadores
                if (info[0].Equals("2"))
                {
                    int.TryParse(info[1], out score1);
                   
                }
                if (info[0].Equals("5"))
                {
                    int.TryParse(info[1], out score2);
                   
                }
                //controle jogo (start2)
                if (info[0].Equals("ç"))
                {
                    int.TryParse(info[1], out i);
                    startingPlayers = true;
                }
                //enviar posicao 1
                if (info[0].Equals("1"))
                {
                    
                    if (cod == 2)
                    {
                        //string[] d = dados.Split(';')[2].Split('.');
                        Debug.Log("dados: " + dados);
                        string[] info2 = info[1].Split('.');
                        Debug.Log("dados: " + dados);
                        Debug.Log("info[0]: " + info[0]);
                        Debug.Log("info[1]: " + info[1]);

                        Debug.Log("info2[1]: " + info2[1]);
                        Debug.Log("cheguei no 1");
                        Debug.Log("sou cod 2");
                        Debug.Log("info2[0]: " + info2[0]);
                    x = float.Parse(info2[0]);
                        Debug.Log("x: " + x);
                        y = float.Parse(info2[1]);
                    z = float.Parse(info2[2]);

                        posicaoCheck = true;

                       // PlayerController.instance.trans.position = new Vector3(x, y, z);
                        //Debug.Log("position"+ PlayerController.instance.trans.position);
                    }
                }
                if (info[0].Equals("4"))
                {
                    
                   
                    if (cod == 1)
                    {
                        string[] info2 = info[1].Split('.');
                        x = float.Parse(info2[0]);
                        y = float.Parse(info2[1]);
                        z = float.Parse(info2[2]);

                        posicao2Check = true;

                    }
                }
                //jogar contole i++ n precisa
                // pegar pontoMax
                if (info[0].Equals("m"))
                {
                    maxScore = 0;
                    int.TryParse(info[1], out maxScore);
                    int.TryParse(score, out currentScore);
                    /*  if (vencedor == 1)
                      {
                          if (cod == 1)
                          {
                              currentScore = currentScore + 5;
                          }
                      }
                      if (vencedor == 2)
                      {
                          if (cod == 2)
                          {
                              currentScore = currentScore + 5;
                          }
                      }*/
                    if (currentScore > maxScore)
                    {
                        maxScoreCheck = 1;
                    }
                    else
                    {
                        maxScoreCheck = 2;
                    }
                }
                //inserir pontuacao maxima n precisa
                //rank
                if (info[0].Equals("r"))
                {
                    
                    rankCheck = true;
                    Debug.Log(rankCheck);
                    string[] info2 = info[1].Split('.');
                    Debug.Log("info2 " + info2[0]);
                    for (int i = 0; i < 7; i++)
                    {
                        rankArray[i] = info2[i];
                        Debug.Log(rankArray[i]);
                    }
                    
                    
                }
                //deletar n prescisa

            }
            catch
            {
                running = false;
            }
        }
    }
    private void OnApplicationQuit()
    {
        Desconectar();
        Debug.Log("AQUI!");
    }
    public void Desconectar()
    {
        running = false;
        if (thread != null)
        {
            thread.Abort();
        }
        cliente = null;
    }
    public void cadastro()
    {
            usuario = InputController.instance.usuarioInput.text;
            senha = int.Parse(InputController.instance.senhaInput.text);
            EnviarMsg("c", $"{usuario}.{senha}");
    }
    public void login()
    {
            usuario = InputController.instance.usuarioLInput.text;
            senha = int.Parse(InputController.instance.senhaLInput.text);
            EnviarMsg("l", $"{usuario}.{senha}");
    }
    public void desconexao()
    {
            EnviarMsg("d", $"desconectar");
            Desconectar();

    }
    public void pontuar(int soma)
    {         
            score += soma;
            EnviarMsg("p", $"{usuarioLogado}.{score}");
    }
    public void inserirObsCol(int obs)
    {
            EnviarMsg("o", $"{usuarioLogado}.{obs}");
    }
    public void inserirCol(int col)
    {
            EnviarMsg("i", $"{usuarioLogado}.{col}");
    }
    public void pegarPonto()
    {
        EnviarMsg("s", $"{usuarioLogado}.score");
       }
    public void enviarScore1()
    {
            if (cod == 1)
            {
                EnviarMsg("y", $"{score}");
            }
           
    }
    public void enviarScore2()
    {
            if (cod == 2)
            {
                EnviarMsg("u", $"{score}");
            }

    }
    public void finishGame()
    {
       
        EnviarMsg("b", $"chegou");
    }
    public void contadorFinish()
    {
            EnviarMsg("3", $"chegou");
    }
    public void pegarID()
    {
            EnviarMsg("z", $"{usuarioLogado}.id");
    }
    public void pegarNomeJogadores()
    {
            EnviarMsg("0", $"nomes");
    }
    public void pegarScoreJogador1()
    {
        
            EnviarMsg("2", $"{jogador1}.pontos");
    }
    public void pegarScoreJogador2()
    {

        EnviarMsg("5", $"{jogador2}.pontos");
    }
    public void start2()
    {
        EnviarMsg("ç", $"{usuarioLogado}.start");
    }
    public void enviarPosicao1()
    {
            if (cod == 1)
            {
                EnviarMsg("1", $"{ PlayerController.instance.xUpdate}.{ PlayerController.instance.yUpdate}.{ PlayerController.instance.zUpdate}");
            }
    }
    public void enviarPosicao2()
    {
        if (cod == 2)
        {
            EnviarMsg("4", $"{ PlayerController2.instance.xUpdate}.{ PlayerController2.instance.yUpdate}.{ PlayerController2.instance.zUpdate}");
        }
    }
    public void jogar()
    {
            EnviarMsg("j", $"{usuarioLogado}.jogar");
    }
    public void pegarPontoMax()
    {
            EnviarMsg("m", $"{usuarioLogado}.pontoMax");
    }
    public void inserirPonto()
    {
            EnviarMsg("a", $"{usuarioLogado}.{score}");
    }
    public void rank()
    {
            EnviarMsg("r", $"ranking");
    }
    public void deletarUsuario()
    {
        EnviarMsg("e", $"{usuarioLogado}.excluir");
    }
    public void Conectar()
    {
        if (running) return;
        try
        {

            Debug.Log("Start Client");
            running = true;

            float ndNumero = rnd.Next();
            thread = new Thread(Run);
            thread.Start();
        }
        catch (System.Exception e)
        {
            Debug.Log($"Erro no clinte {e}");
        }
    }

    public void EnviarMsg(string tag, string msg)
    {
        try
        {
            writer.WriteLine($"{tag};{msg}");
            writer.Flush();
        }
        catch (System.Exception e)
        {
            Debug.Log($"Erro EnviarMsg: {e}");
        }
    }
}
