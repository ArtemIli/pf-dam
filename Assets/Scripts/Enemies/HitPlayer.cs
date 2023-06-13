using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{

    public float damage = 1f;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<EstadoPlayer>().PerderVida(damage);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        
    }

}
