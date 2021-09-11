using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public Text Score;
    public Text Wave;

    public void UpdateScoreText(float score)
    {
        Score.text = "SCORE: " + score;
    }
    public void UpdateWaveText(int wave)
    {
        Wave.text = "WAVE: " + wave;
    }

}
