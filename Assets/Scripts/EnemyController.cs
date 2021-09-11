using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyController : MonoBehaviour
{
    public float movementSpeed;
    public float pushRadius;

    private Rigidbody m_Rb;
    private GameObject m_FollowTarget;
    private bool m_IsRecharge;

    void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
        AddCircle();
        m_IsRecharge = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_FollowTarget = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        Vector3 moveTowards =
            m_FollowTarget.transform.position - transform.position;
        moveTowards.y = 0;
        m_Rb.AddForce(moveTowards.normalized * movementSpeed);

        if (Mathf.Abs(moveTowards.magnitude) <= pushRadius && m_IsRecharge)
        {
            m_IsRecharge = false;
            m_Rb.AddForce(
                moveTowards.normalized * movementSpeed * 1.2f,
                ForceMode.Impulse);
            Invoke(nameof(Recharge), 2.0f);

        }

        if (transform.position.y <= -15.0f) Destroy(gameObject); 
    }

    private void AddCircle()
    {
        GameObject circleGo = new GameObject
        {
            name = "Circle"
        };
        Vector3 circlePos = Vector3.zero;
        circlePos.y = -0.49f;

        circleGo.transform.parent = transform;
        circleGo.transform.localPosition = circlePos;
        circleGo.DrawCircle(pushRadius, 0.02f);
    }

    private void Recharge()
    {
        m_IsRecharge = true;
        Debug.Log("[charge]");
    }
}
