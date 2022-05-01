using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 7;
    public float rotationSpeed = 250;
    public float fuerzaPutiaso = 10;
    public Animator animator;

    private bool play;
    public bool freeMovement = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float x, y;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       initialPosition = transform.position;
       initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Obté cap a on es mou el personatge
        if(play) {
            if(freeMovement) {
                x = Input.GetAxis("Horizontal");
                y = Input.GetAxis("Vertical");
            }
            else {
                if(Input.GetKey(KeyCode.A)) y = 1;
                else y = 0;
                if(Input.GetKeyUp(KeyCode.A)) x = 0;
            }
            // Moviment personatge
            transform.Rotate(0,x * rotationSpeed * Time.deltaTime, 0); // Rota el personatge
            
            transform.Translate(0, 0 ,y * runSpeed * Time.deltaTime); // Mou el personatge

        }
            animator.SetFloat("speedX", x);
            animator.SetFloat("speedY", y);
        
    } 

    public void setPlaying(bool b) {
        play = b;
    }  

    public void changePlaying() {
        play = !play;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Respawn") {
            Debug.Log("Triggered by Death");
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            x = 0;
        }
        else if(other.tag == "TurnRight") {
            Debug.Log("Triggered by Turn");
            x = 1;
        }
        else if(other.tag == "StopTurn") {
            Debug.Log("Triggered by StopTurn");
            x = 0;
        }
    }

    void OnCollisionEnter(Collision collision) {
        Collider c = collision.collider;
        Debug.Log(c.tag);
        if(c.tag == "Putiaso") {
            Debug.Log("Tremendo putiaso me has dado");
            rb.AddForce(fuerzaPutiaso*collision.contacts[0].normal, ForceMode.Impulse);
            x = 0;
        }
    }

}
