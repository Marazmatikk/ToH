using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//контроль генерациии определённых объектов
public class ChallengeController : MonoBehaviour
{
    [SerializeField] 
    GameData gameData;
    
    [SerializeField] 
    Camera camera;
    
    [SerializeField]  
    GameObject riverPoint;
    
    [SerializeField] 
    Transform cameraTarget;
    
    [SerializeField]  
    GameObject far_grounds;
    
    [SerializeField] 
    GameObject phoenix;
    
    [SerializeField]  
    GameObject[] tree;
    
    [SerializeField] 
    GameObject[] enemies;
    
    [SerializeField]
    GameObject[] challenges;
    
    [SerializeField] 
    Transform spawnPoint;
    
    [SerializeField]  
    Transform spawnPointTree;

    [SerializeField] 
    GameObject parentPlatforms;
    
    [SerializeField] 
    GameObject parentEnemies;
    
    [SerializeField]
    GameObject parentGreenery;
    
    [SerializeField] 
    CheckGround playerCheck;
    
    [SerializeField] 
    float timeSpawn = 3.0f;
    
    [SerializeField]  
    float moveUp = 1.3f;
    
    [SerializeField]
    float maxdist;
    
    SpriteRenderer renderer;
    
    GameObject currentChild;
    
    public GameObject lastChild;
    //предыдущий рандом, чтобы знать что было перед этим
    int prev = 5;
    float waitSecond = 2f;
    float timePhx;
    float constTimePhx = 20f;
    float constTimeFG;
    float timeFG = 30;
    
    float        newSpawnPoint;
    int          random;
    int          random_enemies;
    float        currentTimeSpawn;
    float        size;
    public float difference;
    public float difMultiple;
    int          random_most;
    bool         block    = false;
    int          rndTree  = 0;
    int          prevTree = 2;
    

    void Start()
    {
        Generate_Random_Challenge();
        currentTimeSpawn = timeSpawn;
        timePhx = constTimePhx;
        constTimeFG = Random.Range(20f, 30f);
        timeFG = constTimeFG;
        newSpawnPoint = maxdist;
    }

    void Update ()
    {
        if (!GameData.isGameOver)
        {
            Timer();
            Generate_Random_Challenge();
            Generate_Backgroung();
            Generate_Phoenix();
        }             
    }

    void Generate_Random_Challenge()
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

    void Generate_Enemy()
    {
        if (Random.Range(0, 3) == 0 && random != 1 && currentTimeSpawn <= 0)
        {
            random_enemies = Random.Range(0, enemies.Length);
            Vector3 spawn = new Vector3(spawnPointTree.position.x + Random.Range(0.5f, challenges[random].GetComponent<SpriteRenderer>().size.x - 0.4f), spawnPointTree.position.y, spawnPointTree.position.z - 0.01f);
            Instantiate(enemies[random_enemies], spawn, Quaternion.identity, parentEnemies.transform);
            currentTimeSpawn = timeSpawn;
        }
    }

    void Generate_Phoenix()
    {
        if(timePhx <= 0 && Random.Range(0,10) == 0)
        {
            Instantiate(phoenix, spawnPointTree.position + Vector3.up * 4.6f, Quaternion.identity, parentEnemies.transform);
            timePhx = constTimePhx;
        }
    }

    //поднимает позицию спауна и позицию по игрику, относительно которой игрок умрёт по оси "Y"
    void Change_Spawn_Position()
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
    void Generate_Tree()
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

    void Generate_Backgroung()
    {
        if(timeFG<=0)
        {
            Instantiate(far_grounds, riverPoint.transform.position + Vector3.forward*0.21f, Quaternion.identity, riverPoint.transform);
            constTimeFG = Random.Range(30, 50);
            timeFG = constTimeFG;
        }
    }

    void Timer()
    {
        timeFG -= Time.deltaTime;
        timePhx -= Time.deltaTime;
        currentTimeSpawn -= Time.deltaTime;
    }
}
