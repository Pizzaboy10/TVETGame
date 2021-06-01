using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    //The gameobjecton the canvas
    public Image heart1, heart2, heart3;  
    
    //The actual files in the project
    public Sprite fullHeart, brokenheart; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealthChange(int health)
    {
        if(health == 0)
        {
            heart1.sprite = brokenheart;
            heart2.sprite = brokenheart;
            heart3.sprite = brokenheart;
        }

        if (health == 1)
        {
            heart1.sprite = fullHeart;
            heart2.sprite = brokenheart;
            heart3.sprite = brokenheart;
        }
        
        if (health == 2)
        {
            heart1.sprite = fullHeart;
            heart2.sprite = fullHeart;
            heart3.sprite = brokenheart;
        }
        
        if (health == 3)
        {
            heart1.sprite = fullHeart;
            heart2.sprite = fullHeart;
            heart3.sprite = fullHeart;
        }

    }

}
