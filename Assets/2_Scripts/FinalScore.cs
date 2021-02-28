using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    [SerializeField] Text ScoreText;
    void Start()
    {
        ScoreText.text = "Score: " + FindObjectOfType<GameMana>().Score.ToString();
    }
}
