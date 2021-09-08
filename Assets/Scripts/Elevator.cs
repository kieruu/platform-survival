using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    private float m_MaxTravelDistance = 11f;
    private float m_TravelDistance = 0;

    private float m_Speed = 3.0f;

    private Coroutine m_ReverseCoroutine;

    IEnumerator Start()
    {
        enabled = false;
        yield return new WaitForSeconds(3.0f);
        enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_TravelDistance >= m_MaxTravelDistance)
        {
            if(m_ReverseCoroutine == null)
            {
                // reverse the speed of elevator vice versa 
                m_ReverseCoroutine = StartCoroutine(nameof(ReverseElevator));
            }
        }
        else
        {
            float distanceStep = m_Speed * Time.fixedDeltaTime;
            m_TravelDistance += Mathf.Abs(distanceStep);
            transform.Translate(0, distanceStep, 0);
        }
    }

    private IEnumerator ReverseElevator()
    {
        // delay execution below for n seconds
        yield return new WaitForSeconds(3.0f);
        m_TravelDistance = 0; 
        m_Speed = -m_Speed;
        m_ReverseCoroutine = null;
    }

    
}
