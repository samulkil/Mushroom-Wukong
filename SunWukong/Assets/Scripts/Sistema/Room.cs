using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeWhenEntered, openWhenEnemiesCleared;

    public GameObject[] doors;

    public List<GameObject> enemies = new List<GameObject>();

    [HideInInspector]
    public bool roomActive;

    void Start()
    {
        // Desativa as portas automaticamente se não houver inimigos na sala
        if (enemies.Count == 0)
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(false); // Remove visualmente as portas
            }
        }
    }

    void Update()
    {
        // Verifica a contagem de inimigos enquanto a sala está ativa
        if (enemies.Count > 0 && roomActive && openWhenEnemiesCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            // Abre as portas se todos os inimigos forem derrotados
            if (enemies.Count == 0)
            {
                OpenDoors();
            }
        }
    }

    public void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false); // Abre as portas
        }
    }

    public void CloseDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(true); // Fecha as portas
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);

            // Fecha as portas apenas se houver inimigos na sala
            if (enemies.Count > 0)
            {
                CloseDoors();
            }

            roomActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            roomActive = false;
        }
    }
}
