using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager1 : MonoBehaviour
{
    public Image heart1, heart2, heart3;
    public Sprite fullHeart, brokenHeart;



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
            heart1.sprite = brokenHeart;
            heart2.sprite = brokenHeart;
            heart3.sprite = brokenHeart;
        }
        if(health == 1)
        {
            heart1.sprite = fullHeart;
            heart2.sprite = brokenHeart;
            heart3.sprite = brokenHeart;
        }
        if(health == 2)
        {
            heart1.sprite = fullHeart;
            heart2.sprite = fullHeart;
            heart3.sprite = brokenHeart;
        }
        if(health == 3)
        {
            heart1.sprite = fullHeart;
            heart2.sprite = fullHeart;
            heart3.sprite = fullHeart;
        }

    }


}
