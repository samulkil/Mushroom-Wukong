using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Referência ao Animator para animações
   

public class Mushroom : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rigidBody;
    public float moveSpeed;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    void Start()
    {
        // Inicializa referências
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator não encontrado no GameObject!");
        }

    }

    private void Run()
    {
        // Dispara a animação de ataque
        animator.SetTrigger("Run");
    }

        private void StopRun()
    {
        // Dispara a animação de ataque
        animator.SetTrigger("StopRunning");
    }

    void Update(){
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            Run();
            // Calcula a dire��o do movimento
            moveDirection = PlayerController.instance.transform.position - transform.position;

            // Verifica se o inimigo deve virar
            if (moveDirection.x < 0)
            {
                // Vira para a direita
                transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            }
            else if (moveDirection.x > 0)
            {
                // Vira para a esquerda
                transform.localScale = new Vector3(-0.7f, 0.7f, 1f);
            }
        }
        else
        {
            moveDirection = Vector3.zero;
            StopRun();
        }

        moveDirection.Normalize();

        

        // Move o inimigo
        rigidBody.velocity = moveDirection * moveSpeed;
    }  


}

