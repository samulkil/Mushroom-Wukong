using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int damageDone = 10;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public bool isDefending = false; // Flag para verificar se está defendendo
    public float defenseSpeedReduction = 0.5f; // Redução de velocidade ao defender
    public float normalSpeed = 5f; // Velocidade normal do jogador
    public float defenseSpeed = 2.5f; // Velocidade reduzida ao defender

    private PlayerController playerMovement; // Referência ao script de movimento do jogador

    public static PlayerCombat instance;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerController>(); // Obtém o script de movimento do jogador
    }

    void Update()
    {
        // Atacar
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        // Defesa
        if (Input.GetKey(KeyCode.F))
        {
            StartDefense();
        }
        else
        {
            StopDefense();
        }
    }

    void Attack()
    {
        // Rodar a animação de ataque
        animator.SetTrigger("Attack");
        AudioManager.instance.PlaySFX(1);

        // Detectar inimigo no range do ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Dar dano no inimigo
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyCombat>().TakeDamage(damageDone);
        }
    }

    void StartDefense()
    {
        if (!isDefending)
        {
            isDefending = true;
            playerMovement.moveSpeed = defenseSpeed; // Reduz a velocidade ao defender
        }
    }

    void StopDefense()
    {
        if (isDefending)
        {
            isDefending = false;
            playerMovement.moveSpeed = normalSpeed; // Restaura a velocidade normal
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
