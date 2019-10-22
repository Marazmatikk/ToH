using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//контроль генерациии определённых объектов
public class ChallengeController : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject riverPoint;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private GameObject far_grounds;
    [SerializeField] private GameObject phoenix;
    [SerializeField] private GameObject[] tree;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] challenges;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform spawnPointTree;
    [SerializeField] private float timeSpawn = 3.0f;
    [SerializeField] private float moveUp = 1.3f;
    [SerializeField] private GameObject parentPlatforms;
    [SerializeField] private GameObject parentEnemies;
    [SerializeField] private GameObject parentGreenery;
    [SerializeField] private CheckGround playerCheck;
    private float newSpawnPoint;
    private int random;
    private int random_enemies;
    private float currentTimeSpawn;
    private float size;
    public float difference;
    public float difMultiple;
    private int random_most;
    private bool block = false;
    private int rndTree = 0;
    private int prevTree = 2;
    [SerializeField] private float maxdist;
    private new SpriteRenderer renderer;
    private GameObject currentChild;
    public GameObject lastChild;
    //предыдущий рандом, чтобы знать что было перед этим
    private int prev = 5;
    private float waitSecond = 2f;
    private float timePhx;
    private float constTimePhx = 20f;
    private float constTimeFG;
    private float timeFG = 30;

    private void Start()
    {
        Generate_Random_Challenge();
        currentTimeSpawn = timeSpawn;
        timePhx = constTimePhx;
        constTimeFG = Random.Range(20f, 30f);
        timeFG = constTimeFG;
        newSpawnPoint = maxdist;
    }

    private void Update ()
    {
        if (!GameData.isGameOver)
        {
            Timer();
            Generate_Random_Challenge();
            Generate_Backgroung();
            Generate_Phoenix();
        }             
    }

    private void Generate_Random_Challenge()
    {
        if (lastChild.transform.position.x <= newSpawnPoint + difference)
        {
            if (block)
            {
                random = random_most;
            }
            else
            {
                //последний
                if (prev == challenges.Length - 1)
                {
                    random = Random.Range(0, 2);
                }
                //мост и левитирующая земля
                if (prev == 1 || prev == 2)
                {
                    random = 0;
                }
                //начало и середины
                if (prev == 0 || prev == 3 || prev == 4)
                {
                    random = Random.Range(3, challenges.Length);
                    //генерация окружения
                    Generate_Tree();
                }
            }
            block = false;
            //генерация врагов
            Generate_Enemy();
            //создание платформы
            if (random == 1 || random == 2)
            {
                lastChild = Instantiate(challenges[random], spawnPointTree.position, Quaternion.identity, parentPlatforms.transform) as GameObject;
            }
            else
            {
                lastChild = Instantiate(challenges[random], spawnPoint.position, Quaternion.identity, parentPlatforms.transform) as GameObject;
            }
            if (random == 1)
            {
                Change_Spawn_Position();
            }
            //finish ground и earth (определение расстояния м/у платформами)
            if (random == challenges.Length - 1 || random == 1)
            {
                random_most = Random.Range(0, 3);
                if (random_most == 2 && random == challenges.Length - 1)
                {
                    block = true;
                    difference = 0.2f * difMultiple;
                }
                else
                {
                    difference = -maxdist;
                }
            }
            else
            {
                if (random == 2)
                {
                    difference = 0.2f * difMultiple;
                }
                else
                {
                    difference = 0.2f * difMultiple;
                }
            }
            prev = random;
            renderer = lastChild.GetComponent<SpriteRenderer>();
            size = renderer.size.x;
            newSpawnPoint = spawnPoint.position.x - size;
        }
        else return;
    }

    private void Generate_Enemy()
    {
        if (Random.Range(0, 3) == 0 && random != 1 && currentTimeSpawn <= 0)
        {
            random_enemies = Random.Range(0, enemies.Length);
            Vector3 spawn = new Vector3(spawnPointTree.position.x + Random.Range(0.5f, challenges[random].GetComponent<SpriteRenderer>().size.x - 0.4f), spawnPointTree.position.y, spawnPointTree.position.z - 0.01f);
            Instantiate(enemies[random_enemies], spawn, Quaternion.identity, parentEnemies.transform);
            currentTimeSpawn = timeSpawn;
        }
    }

    private void Generate_Phoenix()
    {
        if(timePhx <= 0 && Random.Range(0,10) == 0)
        {
            Instantiate(phoenix, spawnPointTree.position + Vector3.up * 4.6f, Quaternion.identity, parentEnemies.transform);
            timePhx = constTimePhx;
        }
    }

    //поднимает позицию спауна и позицию по игрику, относительно которой игрок умрёт по оси "Y"
    private void Change_Spawn_Position()
    {
        spawnPoint.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y + moveUp);
        StartCoroutine(WaitAndMoveUp(waitSecond));
    }

    IEnumerator WaitAndMoveUp(float second)
    {
        yield return new WaitForSeconds(second);
        playerCheck.diePosY += moveUp;
        cameraTarget.position += Vector3.up * moveUp;
    }

    //Генерация деревьев
    private void Generate_Tree()
    {
        if (Random.Range(0, 3) != 0)
        {
            while(prevTree == rndTree)
            {
                rndTree = Random.Range(0, tree.Length);
            }
            prevTree = rndTree;
            spawnPointTree.position = new Vector3(spawnPointTree.position.x + challenges[random].GetComponent<SpriteRenderer>().size.x / 2, spawnPointTree.position.y, 0.15f);
            Instantiate(tree[rndTree], spawnPointTree.position, Quaternion.identity, parentGreenery.transform);
            spawnPointTree.position = new Vector3(spawnPointTree.position.x - challenges[random].GetComponent<SpriteRenderer>().size.x / 2, spawnPointTree.position.y, 0);
        }
    }

    private void Generate_Backgroung()
    {
        if(timeFG<=0)
        {
            Instantiate(far_grounds, riverPoint.transform.position + Vector3.forward*0.21f, Quaternion.identity, riverPoint.transform);
            constTimeFG = Random.Range(30, 50);
            timeFG = constTimeFG;
        }
    }

    private void Timer()
    {
        timeFG -= Time.deltaTime;
        timePhx -= Time.deltaTime;
        currentTimeSpawn -= Time.deltaTime;
    }
}
