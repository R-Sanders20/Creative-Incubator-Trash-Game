using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class endScene : MonoBehaviour
{
    public TextMeshProUGUI finalScore;
    

    public void Start()
    {
        int receivedPoints = SpawnCollectable.score;
        finalScore.text = "Final Score:" + receivedPoints.ToString();
    }
}
