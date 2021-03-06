using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance; 

    [HideInInspector]
    public int sheepSaved; 

    [HideInInspector]
    public int sheepDropped;

    public int sheepDroppedBeforeGameOver; 
    public SheepSpawner sheepSpawner;

    public Sheep sheep;
    public int level;

    public AudioClip levelUpSound;
    private AudioSource soundPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        soundPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void SavedSheep()
    {
        sheepSaved++;
        UIManager.Instance.UpdateSheepSaved();
        if(sheepSaved == level*5)
        {
            sheepSpawner.DestroyAllSheep();
            soundPlayer.PlayOneShot(levelUpSound, 1.0f);
            sheep.runSpeed = sheep.runSpeed * 2;
            level++;
            Debug.Log("LevelUp");
        }
    }

    private void GameOver()
    {
        sheepSpawner.canSpawn = false; 
        sheepSpawner.DestroyAllSheep();
        UIManager.Instance.ShowGameOverWindow();
        sheep.runSpeed = 3;
    }

    public void DroppedSheep()
    {
        sheepDropped++;
        UIManager.Instance.UpdateSheepDropped();

        if (sheepDropped == sheepDroppedBeforeGameOver) 
        {
            GameOver();
        }
    }
}
