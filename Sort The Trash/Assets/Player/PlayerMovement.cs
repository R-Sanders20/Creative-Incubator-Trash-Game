using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public GameObject cameraMovement;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        
       // Vector3 cameraDirection = new Vector3(horizontalInput, 0, verticalInput);
       // transform.Translate(cameraDirection * speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }

        //  Vector3 cameraMovement = new Vector3(movementDirection.x, 0, movementDirection.z); 
        // cameraMovement.Normalize();

        // transform.Translate(cameraMovement * speed * Time.deltaTime, Space.World);
    }
}
