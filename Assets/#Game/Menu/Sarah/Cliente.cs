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
    int senha;
    int pontos;
    public string score;
    private TcpClient cliente;
    private StreamReader reader = null;
    private StreamWriter writer = null;
    private Thread thread;
    private bool running = false;
    System.Random rnd;
    int maxScore;
    int currentScore;
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
        if (SceneManager.GetActiveScene().name.Equals("Gameplay"))
        {
            gameplay = true;
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

    private void Run()
    {
        cliente = new TcpClient("127.0.0.1", 5000);
        NetworkStream stream = cliente.GetStream();

        reader = new StreamReader(stream);
        writer = new StreamWriter(stream);
        string dados;
        //cadastro();
        //login();
        //pegarPonto();
        //pontuar();
        
        while (running)
        {
            try
            {

                Thread.Sleep(1000);

            }
            catch
            {
                running = false;
            }
        }
    }

    public void cadastro()
    {
        string dados;
        do
        {
            usuario = InputController.instance.usuarioInput.text;
            senha = int.Parse(InputController.instance.senhaInput.text);
            EnviarMsg("c", $"{usuario}.{senha}");
            Thread.Sleep(100);
            Debug.Log(usuario + " WAITING");
            dados = reader.ReadLine();

            if (dados.Equals(Estado.VALIDADO.ToString()))
            {
               
                InputController.instance.msg = "realizado com sucesso";
                MenuController.instance.cadastrarCerto();
               
               // Desconectar();
                break;
            }
            else
            {
                InputController.instance.msg = "Cadastro já existente, tente outro";
                InputController.instance.usuarioInput.text="";
                InputController.instance.senhaInput.text = "";
                break;
            }
        }
        while (running);
    }
    public void login()
    {
        string dados;
        do
        {
            usuario = InputController.instance.usuarioLInput.text;
            senha = int.Parse(InputController.instance.senhaLInput.text);
            EnviarMsg("l", $"{usuario}.{senha}");
            Thread.Sleep(100);
            Debug.Log(usuario + " WAITING");
            dados = reader.ReadLine();

            if (dados.Equals(Estado.SUCESSO.ToString()))
            {
                

                MenuController.instance.logarCerto();
                 usuarioLogado = usuario;
                InputController.instance.msg = ("Login válido: "+usuarioLogado);
               // Desconectar();
                break;
            }
            
            if (dados.Equals(Estado.ERRO.ToString()))
            {
                InputController.instance.msg = "Erro do servidor";
                break;
            }
            if (dados.Equals(Estado.NAOFOIACHADO.ToString()))
            {
                InputController.instance.msg = "Nao foi achado o usuario";
                InputController.instance.usuarioLInput.text = "";
                InputController.instance.senhaLInput.text = "";
                break;
            }
            if (dados.Equals(Estado.SUCESSOCOMERRO.ToString()))
            {
                InputController.instance.msg = "senha incorreta";
                //InputController.instance.usuarioLInput.text = "";
                InputController.instance.senhaLInput.text = "";
                break;
            }
        }
        while (running);
    }

    public void pontuar(int soma)
    {
        string dados;
        do
        {
            pontos += soma;
            EnviarMsg("p", $"{usuarioLogado}.{pontos}");
            Thread.Sleep(100);
            Debug.Log(usuario + " WAITING");
            dados = reader.ReadLine();

            if (dados.Equals(Estado.FOI.ToString()))
            {

                Debug.Log("pontuacao foi pro bd");
                break;
            }
            if (dados.Equals(Estado.NAOFOI.ToString()))
            {

                Debug.Log("pontuacao n foi pro bd");
                break;
            }
            if (dados.Equals(Estado.ERRO.ToString()))
            {

                Debug.Log("deu ruim");
                break;
            }
            else
            {
                break;
            }
        }
        while (running);
    }

    public void inserirObsCol(int obs)
    {
        string dados;
        do
        {
            
            EnviarMsg("o", $"{usuarioLogado}.{obs}");
            Thread.Sleep(100);
            Debug.Log(usuario + " WAITING");
            dados = reader.ReadLine();

            if (dados.Equals(Estado.FOI.ToString()))
            {

                Debug.Log("obs foi pro bd");
                //break;
            }
            if (dados.Equals(Estado.NAOFOI.ToString()))
            {

                Debug.Log("obs n foi pro bd");
                break;
            }
            if (dados.Equals(Estado.ERRO.ToString()))
            {

                Debug.Log("deu ruim");
                break;
            }
            else
            {
                break;
            }
        }
        while (running);
    }

    public void inserirCol(int col)
    {
        string dados;
        do
        {

            EnviarMsg("i", $"{usuarioLogado}.{col}");
            Thread.Sleep(100);
            Debug.Log(usuario + " WAITING");
            dados = reader.ReadLine();

            if (dados.Equals(Estado.FOI.ToString()))
            {

                Debug.Log("col foi pro bd");
                break;
            }
            if (dados.Equals(Estado.NAOFOI.ToString()))
            {

                Debug.Log("col n foi pro bd");
                break;
            }
            if (dados.Equals(Estado.ERRO.ToString()))
            {

                Debug.Log("deu ruim");
                break;
            }
            else
            {
                break;
            }
        }
        while (running);
    }

    public void pegarPonto()
    {
        string dados;
        do
        {

            EnviarMsg("s", $"{usuarioLogado}.score");
            Thread.Sleep(100);
            Debug.Log(usuario + " WAITING");
            dados = reader.ReadLine();
            Debug.Log(dados);
            score = dados;
            GameController.instance.UIScore.text =("Pontos :" + score);
           break;
        }
        while (running);
       }
    public void pegarPontoMax()
    {
        string dados;
        do
        {

            EnviarMsg("m", $"{usuarioLogado}.pontoMax");
            Thread.Sleep(100);
            Debug.Log(usuario + " WAITING");
            dados = reader.ReadLine();
            Debug.Log(dados);

            maxScore = 0;
            int.TryParse(dados, out maxScore);
            int.TryParse(score, out currentScore);

            

            if (currentScore>maxScore)
            {
                Debug.Log("Pontuacao maxima atualizada");
                inserirPonto();
                GameController.instance.currentScore.text = ("Sua pontuacao: " + currentScore.ToString());
                pegarPontoMax();
                GameController.instance.maxScore.text = ("Novo Recorde: " + maxScore.ToString());

            }
            else
            {
                Debug.Log("Pontuacao maxima nao atualizada");
                GameController.instance.currentScore.text = ("Sua pontuacao: " + currentScore.ToString());
                GameController.instance.maxScore.text = ("Seu Recorde: " + maxScore.ToString());
            }
           break;
        }
        while (running);
    }
    public void inserirPonto()
    {
        string dados;
        do
        {

            EnviarMsg("a", $"{usuarioLogado}.{score}");
            Thread.Sleep(100);
            Debug.Log(usuario + " WAITING");
            dados = reader.ReadLine();

            if (dados.Equals(Estado.FOI.ToString()))
            {

                Debug.Log("obs foi pro bd");
                break;
            }
            if (dados.Equals(Estado.NAOFOI.ToString()))
            {

                Debug.Log("obs n foi pro bd");
                break;
            }
            if (dados.Equals(Estado.ERRO.ToString()))
            {

                Debug.Log("deu ruim");
                break;
            }
            else
            {
                break;
            }
        }
        while (running);
    }
    public void rank()
    {
        string dados;
        do
        {

            EnviarMsg("r", $"ranking");
            Thread.Sleep(100);
            Debug.Log(usuario + " WAITING");
            dados = reader.ReadLine();

            string[] info = dados.Split(';');
            int tamamho=info.Length;
            //string[] nomes = info[1].Split(':');
            for(int i=0; i< 7; i++)
            {
                InputController.instance.ranks[i].text = info[i];
            }
            /*InputController.instance.ranks[0].text = info[0];
            InputController.instance.ranks[1].text = info[1];
            InputController.instance.ranks[2].text = info[2];
            InputController.instance.ranks[3].text = info[3];
            InputController.instance.ranks[4].text = info[4];
            InputController.instance.ranks[5].text = info[5];
            InputController.instance.ranks[6].text = info[6];
            */
            break;
            /* EnviarMsg("r", $"quero o rank");
             Thread.Sleep(100);
             Debug.Log(usuario + " WAITING");
             dados = reader.ReadLine();
             if (dados.Equals(Estado.SUCESSO.ToString()))
             {
                 InputController.instance.ranks[0].text = "rank aqui";
             }
             */
            //Thread.Sleep(100);



        }
        while (running);
    }
    public void desconexao()
    {
        do
        {
            EnviarMsg("d", $"desconectar");
            Desconectar();

        } while (running);
    }

    public void deletarUsuario()
    {

        string dados;
        do
        {
            EnviarMsg("e", $"{usuarioLogado}.excluir");
            Thread.Sleep(100);
            dados = reader.ReadLine();
            break;
        } while (running);
    }

    public void Conectar()
    {
        if (running) return;
        try
        {

            Debug.Log("Start Client");
            running = true;

            float ndNumero = rnd.Next();
           // usuario ="Joao";
           // senha = 123;
            thread = new Thread(Run);
            thread.Start();
            //InputController.instance.ranks[0].text = "rank aqui";
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
