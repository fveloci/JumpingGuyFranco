using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int life;
    public GameObject[] hearts;
    private bool dead;
    
 
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {    
        if(dead == true)
        {
            // SET DEAD
        }
    }

    public void TakeDamage(int d)
    {
        life -= d;
        Destroy(hearts[life].gameObject);
        if(life < 1)
        {
            dead = true;
        }
    }
}
