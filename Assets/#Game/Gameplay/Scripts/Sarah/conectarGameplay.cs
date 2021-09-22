using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conectarGameplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cliente.instance.inserirCol(0);
        Cliente.instance.inserirObsCol(0);
       // Cliente.instance.pegarID();

    }
    private void Update()
    {
        //  Cliente.instance.pegarID();
        // Cliente.instance.enviarPosicao1();
    }

    public void pont()
    {
        Cliente.instance.inserirPonto();
    }
    public void conec()
    {
        Cliente.instance.pegarPontoMax();
    }
    public void desconectar()
    {
        Cliente.instance.desconexao();
    }
}
