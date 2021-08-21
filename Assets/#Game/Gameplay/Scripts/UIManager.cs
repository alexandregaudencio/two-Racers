using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{



    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");

    }

    public void GoToGameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }

}
