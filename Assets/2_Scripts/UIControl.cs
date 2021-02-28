using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    [SerializeField] GameObject CreditPan;
    [SerializeField] GameObject HTPPan;
    [SerializeField] GameObject ButtonsPan;
    [SerializeField] GameObject PauseCanvas;

    [SerializeField] InputAction PauseInput;

    private void OnEnable()
    {
        PauseInput.Enable();
    }

    private void OnDisable()
    {
        PauseInput.Disable();
    }

    private void Awake()
    {
        if (PauseCanvas)
        {
            PauseInput.performed += x => OpenPause();
        }
    }

    public void GotoGame()
    {
        SceneManager.LoadScene("Map_Game");
    }

    public void PlayAgain()
    {
        if (FindObjectOfType<GameMana>())
        {
            Destroy(FindObjectOfType<GameMana>());
        }
        GotoGame();
    }
    public void GotoMenu()
    {
        Time.timeScale = 1;
        if(FindObjectOfType<GameMana>())
        {
            Destroy(FindObjectOfType<GameMana>());
        }
        SceneManager.LoadScene("Map_Menu");
    }
    public void GotoEnd()
    {
        SceneManager.LoadScene("Map_End");
    }
    public void OpenCredit()
    {
        if(CreditPan && ButtonsPan)
        {
            ButtonsPan.SetActive(false);
            CreditPan.SetActive(true);
        }
    }
    public void CloseCredit()
    {
        if (CreditPan && ButtonsPan)
        {
            CreditPan.SetActive(false);
            ButtonsPan.SetActive(true);
        }
    }

    public void OpenHTP()
    {
        if (HTPPan && ButtonsPan)
        {
            ButtonsPan.SetActive(false);
            HTPPan.SetActive(true);
        }
    }
    public void CloseHTP()
    {
        if (HTPPan && ButtonsPan)
        {
            HTPPan.SetActive(false);
            ButtonsPan.SetActive(true);
        }
    }

    public void OpenPause()
    {
        if (!PauseCanvas.activeSelf)
        {
            PauseCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            ClosePause();
        }
    }

    public void ClosePause()
    {
        PauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
