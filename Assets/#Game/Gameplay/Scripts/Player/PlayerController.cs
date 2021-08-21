using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float verticalAcceleration;
    [SerializeField] private float horizontalAcceleration;
    [SerializeField] [Range(0,1)] private float traction;

    public float velocityUp;


    private Rigidbody2D carRigidbody;
    private float rotationAngle;
    // Start is called before the first frame update
    void Start()
    {
        carRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButton(Constants.)){  MoveFoward(); }

        //if (Input.GetButton(Constants.backward)){MoveBackward(); }
        MoveFowardBack();
        Tork();
        ApplyTraction();

    


    }

    private void MoveFowardBack()
    {
        velocityUp = Vector2.Dot(transform.up, carRigidbody.velocity);
        //if(velocityUp > maxSpeed && Input.GetAxis(Constants.vertical) > 0.00f)
        //{
        //    return;
        //}
        //if (velocityUp < -maxSpeed*0.5f && Input.GetAxis(Constants.vertical) < 0.00f)
        //{
        //    return;
        //}
        //if (carRigidbody.velocity.sqrMagnitude > maxSpeed*maxSpeed && Input.GetAxis(Constants.vertical) > 0.00f)
        //{
        //    return;
        //}

        

        //if (Input.GetAxis(Constants.vertical) == 0.00 || Input.GetAxis(Constants.vertical) != Mathf.Clamp(velocityUp, -1, 1))
        //{
        //    carRigidbody.drag = Mathf.Lerp(carRigidbody.drag, 3.0f, Time.deltaTime * 5);
        //}else
        //{
        //    carRigidbody.drag = 0;
        //}
        
        float forwardInput = Input.GetAxisRaw(Constants.vertical);
        Vector2 moduleVerticalforce = transform.up * verticalAcceleration * forwardInput;
            
        carRigidbody.AddForce(moduleVerticalforce, ForceMode2D.Force);
    
    }

    private void Tork()
    {
        float afterTurningFactor = (carRigidbody.velocity.magnitude / 8);
        afterTurningFactor = Mathf.Clamp01(afterTurningFactor);
        float horizontalInput = Input.GetAxisRaw(Constants.horizontal);
        
        rotationAngle -= horizontalInput * horizontalAcceleration;
         
        carRigidbody.MoveRotation(rotationAngle);

    }

    private void ApplyTraction()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody.velocity, transform.right);
        carRigidbody.velocity = forwardVelocity + rightVelocity * traction;
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
  
    }




}
