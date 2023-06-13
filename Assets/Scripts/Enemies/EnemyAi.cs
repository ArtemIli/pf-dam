using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{

    public Transform player;
    public float detectionRange = 10f;
    public float movementSpeed = 2f;
    public float raycastOffset = 0.12f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private RaycastHit2D hitInfo;
    private Vector2 direction;
    private Vector2 raycastStart;
    void FixedUpdate()
    {
        //Calcula dirección hacia el jugador
        direction = player.position - transform.position;
        direction.Normalize();

        //Calcula el punto de inicio del raycast
        raycastStart = (Vector2)transform.position + (direction * raycastOffset);

        //Lanza un raycast en la dirección del jugador
        hitInfo = Physics2D.Raycast(raycastStart,direction,detectionRange);

        //Comprueba si el raycast golpea al jugador
        if (hitInfo.collider != null 
            && hitInfo.collider.CompareTag("Player"))
        {
            //Sigue al jugador
            transform.position = Vector2.MoveTowards(transform.position, 
                player.position,
                movementSpeed * Time.fixedDeltaTime);
            if (direction.x < 0)
            {
                spriteRenderer.flipX = false;

            }else if(direction.x > 0)
            {
                spriteRenderer.flipX = true;
            }
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run",false);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastStart,direction * detectionRange);
    }

}
