using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAndVisualEffectsController : MonoBehaviour
{
    [SerializeField] private GameObject smokeParticle;
    [SerializeField] private AudioSource colisionSound;
    [SerializeField] private List<AudioClip> audioClips;


    private void Start()
    {
        colisionSound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag("Tile"))
        {
            Instantiate(smokeParticle, collision.contacts[0].point, Quaternion.identity);
            smokeParticle.GetComponent<ParticleSystem>().Play();
            

            //smokeParticle.transform.position = collision.contacts;
            //smokeParticle.Play();



        }
    }

}
