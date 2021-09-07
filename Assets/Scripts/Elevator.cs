using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    private float m_AccuTime = 0;
    private float m_RunningTime = 3;
    private float m_Speed = 3.0f;

    private Coroutine m_ReverseCoroutine;

    IEnumerator Start()
    {
        enabled = false;
        yield return new WaitForSeconds(3.0f);
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_AccuTime += Time.deltaTime;
        if(m_AccuTime > m_RunningTime)
        {
            if(m_ReverseCoroutine == null)
            {
                // reverse the speed of elevator vice versa 
                m_ReverseCoroutine = StartCoroutine(nameof(ReverseElevator));
            }
        }
        else
        {
            transform.Translate(0, m_Speed * Time.deltaTime, 0);
        }
    }

    private IEnumerator ReverseElevator()
    {
        // delay execution below for n seconds
        yield return new WaitForSeconds(3.0f);

        m_AccuTime = 0;
        m_Speed = -m_Speed;
        m_ReverseCoroutine = null;
    }
}
