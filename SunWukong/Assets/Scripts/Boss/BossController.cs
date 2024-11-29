using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float moveSpeed;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;
    public GameObject levelExit;
    private void Start()
    {
        UIController.instance.bossHealthBar.maxValue = EnemyCombat.instance.health;
        UIController.instance.bossHealthBar.value = EnemyCombat.instance.health;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            // Calcula a direção do movimento
            moveDirection = PlayerController.instance.transform.position - transform.position;

            // Verifica se o inimigo deve virar
            if (moveDirection.x > 0)
            {
                // Vira para a direita
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (moveDirection.x < 0)
            {
                // Vira para a esquerda
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        // Normaliza o vetor de direção
        moveDirection.Normalize();

        // Move o inimigo
        rigidBody.velocity = moveDirection * moveSpeed;

        if(EnemyCombat.instance.health == 0)
        {
            gameObject.SetActive(false);

            if (Vector3.Distance(PlayerController.instance.transform.position, levelExit.transform.position) < 2f)
            {
                levelExit.transform.position += new Vector3(4f, 0f, 0f);
            }

            levelExit.SetActive(true);

            UIController.instance.bossHealthBar.gameObject.SetActive(false);
        }
    }
}
