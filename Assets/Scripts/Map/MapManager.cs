using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MapManager : MonoBehaviour
{
    public List<MapConfig> levelList;

    [SerializeField] Transform mapSpawnPoint;
    [SerializeField] List<GameObject> mapModels;

    public static MapManager instance;

    [SerializeField] bool canCreate;

    GameObject preMapModel;
    float preMapPos;
    float preMapSize;
    float curMapSize;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += createMap;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= createMap;
    }


    void createMap(GameManager.GameState gameState)
    {
        if(canCreate)
        {
            if (gameState == GameManager.GameState.Ready)
            {
                if (mapModels.Count != 0)
                {
                    for (int i = 0; i < mapModels.Count; i++)
                    {
                        Destroy(mapModels[i]);
                    }

                    mapModels.Clear();
                }


                for (int i = 0; i < levelList[GameManager.instance.curLevel].maps.Count; i++)
                {
                    
                    GameObject mapModel = levelList[GameManager.instance.curLevel].maps[i];
                    GameObject map;

                    if(i == 0)
                    {
                        map = Instantiate(mapModel, new Vector3(mapSpawnPoint.position.x,
                        mapSpawnPoint.position.y,
                        0),
                        Quaternion.identity);
                        preMapPos = 0;
                        preMapSize = mapModel.GetComponentInChildren<Renderer>().bounds.size.z;
                        
                    } else
                    {
                        preMapModel = levelList[GameManager.instance.curLevel].maps[i - 1];
                        curMapSize = mapModel.GetComponentInChildren<Renderer>().bounds.size.z;
                        
                        map = Instantiate(mapModel, new Vector3(mapSpawnPoint.position.x,
                        mapSpawnPoint.position.y,
                        preMapPos + (preMapSize + curMapSize) / 2), Quaternion.identity);

                        preMapPos = map.transform.position.z;
                        preMapSize = map.GetComponentInChildren<Renderer>().bounds.size.z;
                    }
                        

                    map.transform.parent = mapSpawnPoint.transform;
                    mapModels.Add(map);
                }
            }
        }
    }
}
