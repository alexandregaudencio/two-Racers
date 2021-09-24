using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public static InputController instance;
    public InputField usuarioInput;
    public InputField senhaInput;
    public InputField usuarioLInput;
    public InputField senhaLInput;
    public Text msgJogadorC;
    public Text msgJogadorL;
    public Text msgJogadorLC;
    public int jogadores;
    public string msg;
    public Text[] ranks;
    public bool comecar;
    public bool apertar;
    
    void Start()
    {
        comecar = false;
        apertar = false;
        instance = this;
        msg = "";
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("jogadores: " + jogadores);
        if (Cliente.instance.i==2 &&apertar==true)
        {
            Cliente.instance.startingPlayers = false;
            SceneManager.LoadScene("Gameplay");
            
        }

        msgJogadorC.text = msg.ToString();
        msgJogadorL.text = msg.ToString();
        msgJogadorLC.text = msg.ToString();
    }
    public void nextLevel()
    {
        // Cliente.instance.Conectar();
        
        //jogadores++;
        // SceneManager.LoadScene("Gameplay");
        apertar = true;
        MenuController.instance.loginokUi.SetActive(false);
        MenuController.instance.aguardeUi.SetActive(true);
    }
}
