using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float moveSpeed;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            // Calcula a direção do movimento
            moveDirection = PlayerController.instance.transform.position - transform.position;

            // Verifica se o inimigo deve virar
            if (moveDirection.x < 0)
            {
                // Vira para a direita
                transform.localScale = new Vector3(5, 5, 1);
            }
            else if (moveDirection.x > 0)
            {
                // Vira para a esquerda
                transform.localScale = new Vector3(-5, 5, 1);
            }
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        moveDirection.Normalize();

        // Move o inimigo
        rigidBody.velocity = moveDirection * moveSpeed;
    }
}
