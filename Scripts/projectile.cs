using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<AutoPlayer>().PlaySound(false, "ProjectileVWall");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
