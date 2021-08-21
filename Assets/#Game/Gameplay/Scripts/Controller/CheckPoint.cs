using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Queue<GameObject> Cars;
    public bool checkedPoint = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            checkedPoint = true;
            //Cars.Enqueue(collision.gameObject);
        }
    }

    




}
