﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float verticalAcceleration;
    [SerializeField] private float horizontalAcceleration;
    [SerializeField] [Range(0,1)] private float traction;
    [SerializeField] private float rockForceReaction;
    public int qntObs = 0;
    public int qntCol = 0;

    //TODO: ver o que fazer com isso;
    public float velocityUp;

    public Rigidbody2D carRigidbody;
    private float rotationAngle;
    public float xUpdate;
    public float yUpdate;
    public float zUpdate;
    GameController gameController;
    public Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
       // carRigidbody = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
        //trans = FindObjectOfType<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        //trans.position = new Vector3(2f, 2f, 0);

        if (Cliente.instance.id % 2 == 1)
        {
            MoveFowardBack();
            Tork();
            ApplyTraction();
            xUpdate = transform.position.x;
            yUpdate = transform.position.y;
            zUpdate = rotationAngle;
        }
       
    }

    private void MoveFowardBack()
    {
     //  Cliente.instance.enviarPosicao1();

         velocityUp = Vector2.Dot(transform.up, carRigidbody.velocity);
        if (velocityUp > maxSpeed && Input.GetAxis(Constants.vertical) > 0.00f)
        {
            return;
        }
        if (velocityUp < -maxSpeed * 0.5f && Input.GetAxis(Constants.vertical) < 0.00f)
        {
            return;
        }
        if (carRigidbody.velocity.sqrMagnitude > maxSpeed * maxSpeed && Input.GetAxis(Constants.vertical) > 0.00f)
        {
            return;
        }



        if (Input.GetAxis(Constants.vertical) == 0.00 || Input.GetAxis(Constants.vertical) != Mathf.Clamp(velocityUp, -1, 1))
        {
            carRigidbody.drag = Mathf.Lerp(carRigidbody.drag, 3.0f, Time.deltaTime * 5);
        }
        else
        {
            carRigidbody.drag = 0;
        }

        float forwardInput = Input.GetAxisRaw(Constants.vertical);
        Vector2 moduleVerticalforce = transform.up * verticalAcceleration * forwardInput;
            
        carRigidbody.AddForce(moduleVerticalforce, ForceMode2D.Force);
    
    }

    private void Tork()
    {
        float afterTurningFactor = (carRigidbody.velocity.magnitude / 8);
        afterTurningFactor = Mathf.Clamp01(afterTurningFactor);
        
        float horizontalInput = Input.GetAxisRaw(Constants.horizontal);
        
        rotationAngle -= horizontalInput * horizontalAcceleration/**afterTurningFactor*/;
         
        carRigidbody.MoveRotation(rotationAngle);

    }

    private void ApplyTraction()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody.velocity, transform.right);
        carRigidbody.velocity = forwardVelocity + rightVelocity * traction;
    }




    //TODO: Tirar daqui?
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Cliente.instance.id % 2 == 1)
        {
            if (collision.gameObject.CompareTag(Constants.collectable))
        {
            
                qntCol++;

                Cliente.instance.inserirCol(qntCol);
                Cliente.instance.pegarPonto();
                //gameController.ChangeUIScore(5);
                Destroy(collision.gameObject);

            }


            else
            {
                if (collision.gameObject.CompareTag(Constants.obstacle))
                {

                    qntObs++;
                    Cliente.instance.inserirObsCol(qntObs);
                    Cliente.instance.pegarPonto();
                    this.carRigidbody.AddForce(transform.up * -1 * velocityUp / 2, ForceMode2D.Impulse);
                    //gameController.ChangeUIScore(-5);
                    Destroy(collision.gameObject);

                }
            }
        }
    }


    float GetLarealVelocity()
    {
        return Vector2.Dot(transform.right, carRigidbody.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBreaking)
    {
        lateralVelocity = GetLarealVelocity();
        isBreaking = false;

        if(Input.GetAxisRaw(Constants.vertical) < 0 && velocityUp > 0)
        {
            isBreaking = true;
            return true;
        }

        if(Mathf.Abs(GetLarealVelocity()) > 0.4f)
        {
            return true;
        }

        return false;
    }










}


