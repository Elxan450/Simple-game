using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterControl : MonoBehaviour
{
    CharacterController charchterController;
    float dX, dZ;

    Vector3 movement = Vector3.zero;

    [SerializeField]
    float speed;

    [SerializeField]
    float gravity;

    [SerializeField]
    float jumpHeight;

    bool jump;

    bool is_reversed;

    void Start()
    {
        speed = 10;
        gravity = 1.2f;
        jumpHeight = 2.5f;
        jump = false;
        is_reversed = false;
        charchterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        dX = Input.GetAxis("Horizontal");
        dZ = Input.GetAxis("Vertical");

        if (!jump && Input.GetKeyDown(KeyCode.Space))
            jump = true;    
    }

    private void FixedUpdate()
    {
        movement = new Vector3(dX, 0, dZ) * speed * Time.fixedDeltaTime;

        if (is_reversed)
        {
            movement.x *= -1;
            movement.z *= -1;
        }

        if (jump)
        {
            movement.y = jumpHeight;
            jump = false;
        }

        if (jump && charchterController.isGrounded)
            movement.y = 0;
        else
            movement.y -= gravity;
             
        charchterController.Move(movement);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Red Sphere")
        {
            GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;
            speed /= 2;
        }

        if (other.gameObject.name == "Green Sphere")
        {
            GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;
            speed *= 2;
        }

        if (other.gameObject.name == "Blue Sphere")
        {
            GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;
            jumpHeight += 5f;
        }

        if (other.gameObject.name == "Black Sphere")
        {
            GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;
            jumpHeight = 0;
        }

        if (other.gameObject.name == "Yellow Sphere")
        {
            GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;
            is_reversed = true;
        }

        other.gameObject.SetActive(false);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "White Sphere")
        {
            GetComponent<Renderer>().material.color = hit.gameObject.GetComponent<Renderer>().material.color;
            var dir = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));

            hit.gameObject.GetComponent<Rigidbody>().AddForce(dir.normalized * Random.Range(1, 10), ForceMode.Impulse);
        }
    }

}
