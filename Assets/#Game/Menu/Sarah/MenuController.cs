using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    [SerializeField] GameObject opcoesUi;
    [SerializeField] GameObject cadastrarUi;
    [SerializeField] GameObject menuUi;
    [SerializeField] GameObject loginUi;
    [SerializeField] GameObject rankUi;
    [SerializeField] GameObject apagadokUi;
    public GameObject cadastroCertoUi;
    public GameObject loginCertoUi;
    public GameObject loginokUi;
    public GameObject aguardeUi;
    
    private void Start()
    {
        
    }
    private void Update()
    {
        //Cliente.instance.start2();
    }
    public void irOpcoes()
    {
        instance = this;
        opcoesUi.SetActive(true);
        menuUi.SetActive(false);
    }
    public void irCadastrar()
    {
        InputController.instance.usuarioInput.text = "";
        InputController.instance.senhaInput.text = "";
        cadastrarUi.SetActive(true);
        cadastroCertoUi.SetActive(true);
        opcoesUi.SetActive(false);
    }
    public void conec()
    {
        Cliente.instance.Conectar();
    }
    public void irLoagr()
    {
        InputController.instance.msg = "";
        InputController.instance.usuarioLInput.text = "";
        InputController.instance.senhaLInput.text = "";
        loginUi.SetActive(true);
        opcoesUi.SetActive(false);
        loginCertoUi.SetActive(true);
    }
    public void irRank()
    {
        rankUi.SetActive(true);
        opcoesUi.SetActive(false);
        loginokUi.SetActive(false);
    }
    public void voltarOpcoes()
    {
        loginUi.SetActive(false);
        rankUi.SetActive(false);
        cadastrarUi.SetActive(false);
        opcoesUi.SetActive(true);
        InputController.instance.msg = "";
        InputController.instance.usuarioLInput.text = "";
        InputController.instance.senhaLInput.text = "";
    }
    public void voltarRank()
    {
        rankUi.SetActive(false);
        loginokUi.SetActive(true);
    }
    public void voltarMenu()
    {
        menuUi.SetActive(true);
        opcoesUi.SetActive(false);
        loginokUi.SetActive(false); 
        apagadokUi.SetActive(false);
    }
    public void apagar()
    {
        loginokUi.SetActive(false);
        apagadokUi.SetActive(true);
    }
    public void cadastrarCerto()
    {
        cadastroCertoUi.SetActive(false);
    }
    public void logarCerto()
    {
        loginCertoUi.SetActive(false);
        loginokUi.SetActive(true);
        loginUi.SetActive(true);
        menuUi.SetActive(false);
    }
}
