    using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class NextLevel : MonoBehaviour
{
    float delay = 3;
    public string message = "Level Complete";
    public Text uiText;
    public bool reload;

    public string[] triggerTags = new string[] { "Player" };

    void Awake()
    {
        Invoke("Clear", delay);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Load();
        }
    }

    void OnTriggerEnter(Collider c)
    {
        foreach (string s in triggerTags)
        {
            if (s != null && c.gameObject.CompareTag(s))
            {
                if (uiText != null)
                {
                    uiText.text = message;
                }

                c.gameObject.SetActive(false);

                Invoke("Load", delay);
                return;
            }
        }
    }

    void Clear()
    {
        uiText.text = "";
    }

    void Load()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        if (index == SceneManager.sceneCountInBuildSettings) { index = 0; }
        if (reload) { index = SceneManager.GetActiveScene().buildIndex; }
        SceneManager.LoadScene(index);
    }
}