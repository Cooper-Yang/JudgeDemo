using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame1 : MonoBehaviour
{
    public GameObject player;
    public Vector3 playerIniPosition;

    public bool paused = false;
    public GameObject PauseSign;

    public float localTime = 1;
    public float maxTime = 4;
    public float accTime = 0.5f;
    public float baseUnit = 1;
    public float baseCounter = 0;
    public int SpawnUnit = 4;
    public int SpawnCounter = 0;

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
        if (!paused)
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
                    enemy.transform.Translate(0, -50, 0);
                }
            }
            foreach (GameObject enemy in Enemies)
            {
                if (enemy.transform.position == player.transform.position)
                {
                    Destroy(gameObject);
                }

                if (enemy.transform.localPosition.y < -125)
                {
                    //print("Removing");
                    Destroy(enemy);
                    Enemies.Remove(enemy);
                    localTime += accTime;
                    localTime = Mathf.Min(localTime, maxTime);
                }
            }
            if (SpawnCounter >= SpawnUnit)
            {
                SpawnCounter = 0;
                GameObject spawn1 = EnemySpawn[Random.Range(0, 3)];
                GameObject spawn2 = EnemySpawn[Random.Range(0, 3)];
                while (spawn2 == spawn1) { spawn2 = EnemySpawn[Random.Range(0, 3)]; }
                Enemies.Add(Instantiate(EnemyOrigin, spawn1.transform.position, spawn1.transform.rotation, gameObject.transform));
                Enemies.Add(Instantiate(EnemyOrigin, spawn2.transform.position, spawn2.transform.rotation, gameObject.transform));
            }
        }
        else
        {
            PauseSign.SetActive(true);
        }
    }
    public void PlayAndPause()
    {
        paused = !paused;
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
