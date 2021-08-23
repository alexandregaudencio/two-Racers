using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToDestroy(timeToDestroy));
        //this.gameObject.GetComponent<Renderer>().sortingOrder = 11;
        this.gameObject.GetComponent<ParticleSystem>().Play();

    }

    // Update is called once per frame
    void Update()
    {
      //if(this.gameObject.GetComponent<ParticleSystem>().isPlaying)
      //  {
      //      Debug.Log("isPlayer a partícle system");
      //  }   
    }

    IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
