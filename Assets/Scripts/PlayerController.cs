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
    private float m_SpeedModifier;

     void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_ElevatorOffsetY = 0;
        m_CameraPos =
            followCamera.transform.position - m_Rb.position;
        m_SpeedModifier = 1;
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
            playerPos.y = 
                m_Elevator.transform.position.y + m_ElevatorOffsetY; 
        }

        // adjust rotation speed w/ 3rd argument
        targetRotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            360 * Time.fixedDeltaTime);

        m_Rb.MovePosition(playerPos + movement * m_SpeedModifier * walkSpeed * Time.fixedDeltaTime);
        m_Rb.MoveRotation(targetRotation);
    }

     void LateUpdate()
    {
        followCamera.transform.position = m_Rb.position + m_CameraPos;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Powerup"))
        {
            Destroy(collision.gameObject);
            m_SpeedModifier = 2;
            StartCoroutine(nameof(BonusSpeedDuration));
        }

        // push enemy when powerup is on.
        if(collision.gameObject.CompareTag("Enemy") && m_SpeedModifier > 1)
        {
            Rigidbody enemyRb = 
                collision.gameObject.GetComponent<Rigidbody>();

            Vector3 awayFromPlayer =
                collision.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayer * 25.0f, ForceMode.Impulse);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Elevator")) 
        {
            m_Elevator = other.gameObject;
            m_ElevatorOffsetY = 
                transform.position.y - m_Elevator.transform.position.y;
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

    private IEnumerator BonusSpeedDuration()
    {
        yield return new WaitForSeconds(3.0f);
        m_SpeedModifier = 1;
    }
}
