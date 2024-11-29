using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] sfx; // Array de clipes de �udio para efeitos sonoros
    private AudioSource audioSource; // Componente de �udio

    private void Awake()
    {
        // Configura��o do Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Garante que o AudioManager persista entre cenas
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Inicializa o AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySFX(int sfxToPlay)
    {
        if (sfxToPlay >= 0 && sfxToPlay < sfx.Length)
        {
            audioSource.PlayOneShot(sfx[sfxToPlay]); // Reproduz o som
        }
        else
        {
            Debug.LogWarning("�ndice de SFX inv�lido: " + sfxToPlay);
        }
    }
}
