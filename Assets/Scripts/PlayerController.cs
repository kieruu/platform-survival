using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;

    private Rigidbody m_Rb;

     void Awake()
    {
        m_Rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(
            horizontalInput, 
            0,
            verticalInput);

        movement.Normalize();
        m_Rb.MovePosition(m_Rb.position + movement * walkSpeed * Time.deltaTime);
    }
}
