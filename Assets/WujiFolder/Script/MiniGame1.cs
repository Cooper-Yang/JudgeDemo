using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame1 : MiniGame
{
    public GameObject player;
    public Vector3 playerIniPosition;

    public bool paused = false;
    public GameObject GamePlay;
    public GameObject PauseSign;
    public GameObject LoseSign;
    public GameObject WinSign;

    public float localTime = 1;
    public float maxTime = 4;
    public float accTime = 0.5f;
    public float baseUnit = 1;
    public float baseCounter = 0;
    public int SpawnUnit = 4;
    public int SpawnCounter = 0;

    public int winNum = 20;
    public int winCount = 0;

    public GameObject EnemyOrigin;
    public GameObject[] EnemySpawn = new GameObject[3];
    public List<GameObject> Enemies;
    // Start is called before the first frame update
    void Start()
    {
        playerIniPosition = player.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused&&!gameOver)
        {
            PauseSign.SetActive(false);

            CheckPlayerInput();
            baseCounter += Time.deltaTime * localTime;
            if (baseCounter >= baseUnit)
            {
                baseCounter = 0;
                SpawnCounter++;
                foreach(GameObject enemy in Enemies)
                {
                    enemy.transform.localPosition = new Vector3(enemy.transform.localPosition.x, enemy.transform.localPosition.y - 50, enemy.transform.localPosition.z);
                }
            }
            foreach (GameObject enemy in Enemies)
            {
                if (enemy.transform.position == player.transform.position)
                {
                    //Destroy(gameObject);
                    Destroy(player);
                    gameOver = true;
                }

                if (enemy.transform.localPosition.y < -125)
                {
                    //print("Removing");
                    Destroy(enemy);
                    Enemies.Remove(enemy);
                    localTime += accTime;
                    winCount++;
                    localTime = Mathf.Min(localTime, maxTime);
                }
            }
            if (SpawnCounter >= SpawnUnit)
            {
                SpawnCounter = 0;
                GameObject spawn1 = EnemySpawn[Random.Range(0, 3)];
                GameObject spawn2 = EnemySpawn[Random.Range(0, 3)];
                while (spawn2 == spawn1) { spawn2 = EnemySpawn[Random.Range(0, 3)]; }
                Enemies.Add(Instantiate(EnemyOrigin, spawn1.transform.position, spawn1.transform.rotation, GamePlay.transform));
                Enemies.Add(Instantiate(EnemyOrigin, spawn2.transform.position, spawn2.transform.rotation, GamePlay.transform));
                /*GameObject spawned1 = Instantiate(EnemyOrigin, spawn1.transform.position, spawn1.transform.rotation, GamePlay.transform);
                GameObject spawned2 = Instantiate(EnemyOrigin, spawn2.transform.position, spawn2.transform.rotation, GamePlay.transform);
                spawned1.transform.localPosition = spawn1.transform.localPosition;
                spawned2.transform.localPosition = spawn2.transform.localPosition;
                Enemies.Add(spawned1); Enemies.Add(spawned2);*/
            }

            if (winCount >= winNum)
            {
                gameOver = true;
            }
        }
        else if(paused)
        {
            PauseSign.SetActive(true);
        }
        else
        {
            WinSign.SetActive(winCount >= winNum);
            win = (winCount >= winNum);
            LoseSign.SetActive(winCount < winNum);
        }
    }
    public void PlayAndPause()
    {
        paused = false;
    }

    void CheckPlayerInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.localPosition = playerIniPosition + new Vector3(-50, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.localPosition = playerIniPosition + new Vector3(50, 0, 0);
        }
        else
        {
            player.transform.localPosition = playerIniPosition;
        }
    }
}
