using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    // Start is called before the first frame update
    public void addAction(string action)
    {

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<AutoPlayer>().actions.Count < 5)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<AutoPlayer>().actions.Enqueue(action);
            GameObject.FindGameObjectWithTag("Player").GetComponent<AutoPlayer>().updateActions();
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<AutoPlayer>().PlaySound(false, "IconClick");
        //Instantiate(Resources.Load(action), Input.mousePosition, transform.rotation);
    }

    public void clearActions()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<AutoPlayer>().actions.Clear();
        GameObject.FindGameObjectWithTag("Player").GetComponent<AutoPlayer>().updateActions();
    }
}
