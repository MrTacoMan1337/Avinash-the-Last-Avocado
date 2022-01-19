using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sounds : MonoBehaviour
{
    public AudioSource music;
    public GameObject sfx;

    public Slider musicSlider;
    public Slider sfxSlider;
    

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicNum");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxNum");
    }

    // Update is called once per frame
    void Update()
    {
        //Music();
        

        

        //musicSlider.value = musicInt;

        if (musicSlider.value == 1)
        {
            music.UnPause();
        }
        else
        {
            music.Pause();
        }

        if (sfxSlider.value == 1)
        {
            sfx.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<AutoPlayer>().fx_muted = false;
        }
        else
        {
            sfx.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<AutoPlayer>().fx_muted = true;
        }

        PlayerPrefs.SetFloat("musicNum", musicSlider.value);
        PlayerPrefs.SetFloat("sfxNum", sfxSlider.value);
    }

    private void Music()
    {
        
    }
}
