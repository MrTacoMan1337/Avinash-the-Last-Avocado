using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject); //Do not destroy object when scenes change
        

    }
    
}
