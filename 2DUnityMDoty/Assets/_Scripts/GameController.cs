using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    UIDisplay uiDisplay;

    public int hitPoints = 10;
    public GameObject[] wave;
    public Transform wavePoint;
    int currentWave = -1;

    public int numberOfEnemies = 0;
    public float spawnDelay = 0.5f;
    float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        uiDisplay = FindObjectOfType<UIDisplay>();
        spawnTimer = spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        uiDisplay.updateHitPoints(hitPoints);
        if (numberOfEnemies <= 0 && spawnTimer <= 0)
        {
            currentWave++;
            if (currentWave <= 3)
            {
                Instantiate(wave[currentWave], wavePoint.position, wavePoint.rotation);
                hitPoints += 5;
                spawnTimer = spawnDelay;
            }
            else
                YouWin();
        }

    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void YouWin()
    {
        SceneManager.LoadScene("YouWin");
    }

    public void GettingHit()
    {
        hitPoints--;
        uiDisplay.updateHitPoints(hitPoints);
        if (hitPoints <= 0)
        {
            GameOver();
        }
    }
}
