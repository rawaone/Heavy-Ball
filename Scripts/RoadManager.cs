using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] GameObject[] roads;
    [SerializeField] float zSpawn = 0;
    [SerializeField] float roadDistance = 10;
    GameManager gameManager;

    [SerializeField] Transform endLinePoint;

    public int numberOfRoads;

    ColorRandomize colorRandomize;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        colorRandomize = FindObjectOfType<ColorRandomize>();

        RoadIncreaseByLevel();

        

        //spawning all roads
        for (int i = 0; i < numberOfRoads; i++)
        {
            if (i == 0)
                SpawnRoad(0);

            int levelConditionIndex = gameManager.currentLevelIndex;
            if (levelConditionIndex >= 1 && levelConditionIndex < 3)
            {
                SpawnRoad(Random.Range(1, 5));
                numberOfRoads = 8;
                //FindObjectOfType<ColorRandomize>().ChangeColorRoad(0);
            }

            else if (levelConditionIndex >= 3 && levelConditionIndex < 5)
            {
                SpawnRoad(Random.Range(2, 6));
                numberOfRoads = 10;
               // FindObjectOfType<ColorRandomize>().ChangeColorRoad(1);
            }
            else if (levelConditionIndex >= 5 && levelConditionIndex < 7)
            {
                SpawnRoad(Random.Range(4, 7));
                numberOfRoads = 12;
                //colorRandomize.ChangeColorRoad(0);
               //FindObjectOfType<ColorRandomize>().ChangeColorRoad(2);
            }
            else if (levelConditionIndex >= 7 && levelConditionIndex < 9)
            {
                SpawnRoad(Random.Range(4, 8));
                numberOfRoads = 14;
                //colorRandomize.ChangeColorRoad(1);
               // FindObjectOfType<ColorRandomize>().ChangeColorRoad(3);
            }
            else if (levelConditionIndex >= 9 && levelConditionIndex < 11)
            {
                SpawnRoad(Random.Range(4, 9));
                numberOfRoads = 16;
                //FindObjectOfType<ColorRandomize>().ChangeColorRoad(4);
            }
            else if (levelConditionIndex >= 11 && levelConditionIndex < 13)
            {
                SpawnRoad(Random.Range(5, 10));
                numberOfRoads = 18;
                //FindObjectOfType<ColorRandomize>().ChangeColorRoad(5);
            }
            else if (levelConditionIndex >= 13 && levelConditionIndex < 15)
            {
                SpawnRoad(Random.Range(4, 11));
                numberOfRoads = 20;
               // FindObjectOfType<ColorRandomize>().ChangeColorRoad(0);
            }
            else if (levelConditionIndex >= 15 && levelConditionIndex < 17)
            {
                SpawnRoad(Random.Range(5, 12));
                numberOfRoads = 22;
               // FindObjectOfType<ColorRandomize>().ChangeColorRoad(1);
            }
            else if (levelConditionIndex >= 17 && levelConditionIndex < 19)
            {
                SpawnRoad(Random.Range(6, 13));
                numberOfRoads = 24;
                //FindObjectOfType<ColorRandomize>().ChangeColorRoad(2);
            }

            else if (levelConditionIndex >= 19 && levelConditionIndex < 21)
            {
                SpawnRoad(Random.Range(4, roads.Length - 1));
                numberOfRoads = 26;
               // FindObjectOfType<ColorRandomize>().ChangeColorRoad(3);
            }
            else
            {
                SpawnRoad(Random.Range(1, roads.Length - 1));
            }
        }

        //spwaning last road
        SpawnRoad(roads.Length - 1);

        //int levelChangeIndex = gameManager.currentLevelIndex;
        //for (int i = 10; i <= levelChangeIndex; i = i + 10)
        //{
        //    if (gameManager.currentLevelIndex >= i && gameManager.currentLevelIndex < i + 10)
        //    {
        //        Debug.Log("We are between level " + i + " and " + (i + 10));
        //        SpawnRoad(Random.Range((i - 9), i));
        //    }
        //}

        endLinePoint.transform.position = new Vector3(0, 0, (numberOfRoads * 10) + 10);


    }

    private void RoadIncreaseByLevel()
    {
        numberOfRoads = roads.Length;

        int levelIncreamentIndex = gameManager.currentLevelIndex;
        for (int i = 10; i <= levelIncreamentIndex; i = i + 10)
        {
            if (gameManager.currentLevelIndex >= i)
            {
                numberOfRoads++;
            }

        }
    }

    public void SpawnRoad(int roadIndex)
    {
        GameObject go = Instantiate(roads[roadIndex], transform.forward * zSpawn, Quaternion.identity);
        go.transform.parent = transform;
        zSpawn += roadDistance;
    }


}
