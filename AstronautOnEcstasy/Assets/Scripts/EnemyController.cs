using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    #region Enemy
    [Header("Enemy")]
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spawnPoint;

    private float startBetweenSpawn = 2.5f;
    private int randSpawn;
    private int rand;
    private float timeBetweenSpawn;
    #endregion

    #region Score
    [SerializeField] TMP_Text scoreText;
    public static float scoreCount = 0f;
    #endregion

    private void Start()
    {
        timeBetweenSpawn = startBetweenSpawn;
    }

    private void LateUpdate()
    {
        SpawnEnemy();
        DisplayText();
    }

    private void SpawnEnemy()
    {
        if (timeBetweenSpawn <= 0)
        {
            rand = Random.Range(0, enemies.Length);
            randSpawn = Random.Range(0, spawnPoint.Length);

            Instantiate(enemies[rand], spawnPoint[randSpawn].transform.position, Quaternion.identity);

            if (scoreCount >= 20)
            {
                startBetweenSpawn = 1.5f;
            }
            if (scoreCount >= 50)
            {
                startBetweenSpawn = 1.25f;
            }
            if (scoreCount >= 80)
            {
                startBetweenSpawn = 1f;
            }
            if (scoreCount >= 100)
            {
                startBetweenSpawn = 0.7f;
            }

            timeBetweenSpawn = startBetweenSpawn;

        }
        timeBetweenSpawn -= Time.deltaTime;

    }

    private void DisplayText()
    {
        scoreText.text = scoreCount.ToString();
    }
}
