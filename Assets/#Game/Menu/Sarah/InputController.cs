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
   
    public string msg;
    public Text[] ranks;
    void Start()
    {
        instance = this;
        msg = "";
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        msgJogadorC.text = msg.ToString();
        msgJogadorL.text = msg.ToString();
        msgJogadorLC.text = msg.ToString();
    }
    public void nextLevel()
    {
       // Cliente.instance.Conectar();
        SceneManager.LoadScene("Gameplay");
        MenuController.instance.loginokUi.SetActive(false);
    }
}
