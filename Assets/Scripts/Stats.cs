using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float score;

    public void UpdateScore(float score)
    {
        this.score += score;
    }
}
