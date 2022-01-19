using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject pauseMenu;
    public GameObject levelPanel;
    public GameObject creditsPanel;
    public AudioSource buttonSound;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        SceneManager.LoadScene("MainMenu");
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Master Scene");
        buttonSound.Play();
    }

    public void MenuButton() //Makes function "MenuButton()"
    {
        Destroy(GameObject.FindGameObjectWithTag("GameManager")); //Destroys the GameObject with tag "GameManager" So that when you click on menu it doesn's have two GameManager objects
        SceneManager.LoadScene("MainMenu");
        buttonSound.Play();
    }

    public void OptionsButton()
    {
        optionsMenu.SetActive(true);
        buttonSound.Play();
    }

    public void UnOptionsButton()
    {
        optionsMenu.SetActive(false);
        buttonSound.Play();
    }

    public void LevelsPanelOn()
    {
        levelPanel.GetComponent<Animator>().SetBool("Play", true);
        buttonSound.Play();
    }

    public void LevelsPanelOff()
    {
        levelPanel.GetComponent<Animator>().SetBool("Play", false);
        buttonSound.Play();
    }

    public void CreditsPanelOn()
    {
        creditsPanel.GetComponent<Animator>().SetBool("Play", true);
        buttonSound.Play();
    }

    public void CreditsPanelOff()
    {
        creditsPanel.GetComponent<Animator>().SetBool("Play", false);
        buttonSound.Play();
    }

    public void PauseButton()
    {
        pauseMenu.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<AutoPlayer>().speed = 0;
        buttonSound.Play();
    }

    public void UnPauseButton()
    {
        pauseMenu.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<AutoPlayer>().speed = 4;
        buttonSound.Play();
    }


}
