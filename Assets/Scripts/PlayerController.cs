using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public Camera followCamera;

    private Rigidbody m_Rb;
    private GameObject m_Elevator;
    private float m_ElevatorOffsetY;
    private Vector3 m_CameraPos;

     void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_ElevatorOffsetY = 0;
        m_CameraPos =
            followCamera.transform.position - m_Rb.position;
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

        if (movement == Vector3.zero) return;

        Vector3 playerPos = m_Rb.position;
        // rotation toward movement
        Quaternion targetRotation =
            Quaternion.LookRotation(movement);

        
        // if player is on the elevator
        if(m_Elevator != null)
        {
            playerPos.y = m_Elevator.transform.position.y + m_ElevatorOffsetY; 
        }

        // adjust rotation speed w/ 3rd argument
        targetRotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            360 * Time.fixedDeltaTime);

        m_Rb.MovePosition(playerPos + movement * walkSpeed * Time.fixedDeltaTime);
        m_Rb.MoveRotation(targetRotation);
    }

     void LateUpdate()
    {
        followCamera.transform.position = m_Rb.position + m_CameraPos;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Elevator")) 
        {
            m_Elevator = other.gameObject;
            m_ElevatorOffsetY = transform.position.y - m_Elevator.transform.position.y;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Elevator"))
        {
            m_Elevator = null;
            m_ElevatorOffsetY = 0;
        }
    }
}
