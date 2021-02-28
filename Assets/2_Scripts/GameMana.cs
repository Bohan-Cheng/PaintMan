using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMana : MonoBehaviour
{
    public float StartCD = 6.0f;
    public int Health = 3;
    public int Score = 0;
    public bool IsGameOver = false;
    public PlayerControl player;
    [SerializeField] DropsManager[] DropsManas;
    [SerializeField] Text CDText;
    [SerializeField] Text ScoreText;
    [SerializeField] Image[] HP;
    [SerializeField] AudioClip clip_CD;
    [SerializeField] AudioClip clip_Start;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        DropsManas = FindObjectsOfType<DropsManager>();
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckHealth()
    {
        if(Health <= 0)
        {
            player.StopPlaying();
            IsGameOver = true;
        }
    }

    public void ResetMap()
    {
        foreach (DropsManager DM in DropsManas)
        {
            DM.ResetDrops();
        }
    }

    IEnumerator CountDown()
    {
        while (StartCD >= 0)
        {
            CDText.text = StartCD.ToString();
            GetComponent<AudioSource>().PlayOneShot(clip_CD);
            StartCD--;
            yield return new WaitForSeconds(1);
        }
        if(StartCD < 0)
        {
            GetComponent<AudioSource>().PlayOneShot(clip_Start);
            CDText.gameObject.SetActive(false);
        }
    }

    void UpdateUI()
    {
        ScoreText.text = Score.ToString();
    }

    public void AddScore(int score)
    {
        Score += score;
        UpdateUI();
    }
}
