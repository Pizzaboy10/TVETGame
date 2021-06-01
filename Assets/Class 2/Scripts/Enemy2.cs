using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public GameObject deathParticle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("DelayedParticle", 0.3f);

        Destroy(gameObject, 0.3f);    //Destroy self when enters a trigger zone
    }

    void DelayedParticle()
    {
        GameObject effect = Instantiate(deathParticle,
                                transform.position,
                                Quaternion.identity);
        Destroy(effect, 1);
    }


}
