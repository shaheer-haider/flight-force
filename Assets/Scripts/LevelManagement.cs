using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LevelManagement : MonoBehaviour
{
    private List<float> xRange = new List<float> { -900f, 900f };
    private List<float> zRange = new List<float> { -900f, 900f };
    public int levelNumber = 0;
    private int groundZWidth = 2000;
    public List<GameObject> obstaclesPrefab = new List<GameObject>();
    public List<Dictionary<string, dynamic>> obstaclesList = new List<Dictionary<string, dynamic>>();
    public LevelManagement instance;

    public enum obstacleEnum
    {
        Tower,
        Pendulum,
    };
    public GameObject ground;
    public GameObject fencesStart;
    public GameObject fencesEnd;
    public GameObject fencesPrefab;
    public GameObject wheelPrefab;
    public GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        zRange[0] = -groundZWidth / 2 + 100;
        zRange[1] = groundZWidth / 2 - 100;

        // set ground size
        ground.transform.localScale = new Vector3(ground.transform.localScale.x, ground.transform.localScale.y, groundZWidth);
        // set start and end fences
        fencesStart.transform.position = new Vector3(fencesStart.transform.position.x, fencesStart.transform.position.y, -groundZWidth / 2 + 25);
        fencesEnd.transform.position = new Vector3(fencesStart.transform.position.x, fencesStart.transform.position.y, groundZWidth / 2 - 25);
        // set player position
        player.transform.position = new Vector3(0, 74, -groundZWidth / 2 - 130);
    }
    void Start()
    {
        updateObstaclesCoroutine();
        createWheel();
        generateSideFences();
    }

    float randomTowerPositionX()
    {
        return Random.Range(xRange[0], xRange[1]);
    }

    float randomTowerPositionZ()
    {
        return Random.Range(zRange[0], zRange[1]);
    }

    void generateSideFences()
    {
        // instantiate side fences
        for (float z = zRange[0] - 52; z < zRange[1] + 52; z += 52)
        {
            Instantiate(fencesPrefab, new Vector3(988, 1.52f, z), Quaternion.Euler(0, 90, 0));
            Instantiate(fencesPrefab, new Vector3(-988, 1.52f, z), Quaternion.Euler(0, -90, 0));
        }
    }

    public void updateObstaclesCoroutine()
    {
        // create obstacles
        int xIncrement = Random.Range(140, 200);
        int zIncrement = Random.Range(140, 200);
        List<int> prefabRanges = new List<int> { };

        for (float x = xRange[0]; x < xRange[1]; x += xIncrement)
        {
            for (float z = zRange[0]; z < zRange[1]; z += zIncrement)
            {
                var random = new System.Random();
                int index = random.Next(obstaclesPrefab.Count);
                GameObject obstacle = Instantiate(obstaclesPrefab[index], new Vector3(x, 0, z), Quaternion.identity);
                if (index == (int)obstacleEnum.Tower)
                {
                    obstacle.transform.localScale = new Vector3(1f, Random.Range(.6f, 2f), 1f);
                }
            }
        }
    }
    void createWheel()
    {
        for (float x = xRange[0] + 200; x < xRange[1]; x += Random.Range(500, 600))
        {
            for (float z = zRange[0] + 200; z < zRange[1]; z += Random.Range(500, 600))
            {
                int rotationRange = Random.Range(0, 2);
                GameObject obstacle = Instantiate(wheelPrefab, new Vector3(x, Random.Range(250, 300), z), Quaternion.Euler(new List<int> { 90, 0 }[rotationRange], 0, 0));
            }
        }
    }
}



