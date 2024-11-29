using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider healthSlider;
    public Text healthText;

    public GameObject deathScreen;

    public GameObject pauseMenu;

    public string menu;

    public Slider bossHealthBar;

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(menu);

        Destroy(PlayerController.instance.gameObject);
    }

    public void Pause()
    {
        LevelManager.instance.PausaVolta();
    }
}
