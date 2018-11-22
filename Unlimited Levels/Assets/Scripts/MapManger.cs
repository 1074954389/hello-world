using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManger : MonoBehaviour {
    public static MapManger _instance; 

    public GameObject[] outwallArray;
    public GameObject[] floorArray;
    public GameObject[] wallArray;
    public GameObject[] energyArray;
    public GameObject[] monsterArray;
    public GameObject exitGo;

    private Transform mapholder;

    private List<Vector2> positionList = new List<Vector2>();

    private GameManger gameManger; 
    
    int maxcout = 10;

    void Awake() {
        _instance = this;
        gameManger = this.GetComponent<GameManger>();
        CreatMap();
    }
       
    public void CreatMap()
    {
        //外墙和地板的生成，（将其作为“Map”的子对象生成）
        Instantiate(exitGo, new Vector2(maxcout - 2, maxcout - 2), Quaternion.identity);
        mapholder = new GameObject("Map").transform;
        for (int x = 0; x < maxcout; x++)
            for (int y = 0; y < maxcout; y++)
            {
                if (x == 0 || x == maxcout - 1 || y == 0 || y == maxcout - 1)
                {
                    GameObject outWallPrfab=RandomPrefab (outwallArray );
                    GameObject go = GameObject.Instantiate(outWallPrfab, new Vector2(x, y), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapholder);
                }
                else
                {
                    GameObject floorPrfab = RandomPrefab(floorArray);
                    GameObject go = GameObject.Instantiate(floorPrfab, new Vector2(x, y), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapholder);
                }


            }
        
        positionList.Clear();//初始化列表
        for (int x = 2; x < maxcout - 2; x++)
            for (int y = 2; y < maxcout - 2; y++)
            {
                positionList.Add(new Vector2(x, y));

            }
        //障碍物的生成
        int wallCount = Random.Range(0, gameManger.level * 2 );
        instantiateGo(wallCount, wallArray);
        //能量的生成
        int energyCount = Random.Range(2 , gameManger.level * 2+1);
        instantiateGo(energyCount, energyArray);
        //怪物的生成
        int monsterCount = gameManger .level /2;
        if(monsterCount<3)
        instantiateGo(monsterCount, monsterArray);
    }
    private void  instantiateGo(int Count,GameObject[] PreFabsArray)
    {
        for (int i = 0; i <Count; i++)
        {
            Vector2 pos = RandomPosition();
            GameObject prefab = RandomPrefab(PreFabsArray);
            GameObject go = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
            go.transform.SetParent(mapholder);
        }
    }
    //障碍物，怪物，宝藏的位置生成
    private Vector2  RandomPosition()
    {
        int posIndex = Random.Range(0, positionList.Count);
        Vector2 pos = positionList[posIndex];
        positionList.RemoveAt(posIndex);
        return pos;
    }
    private GameObject RandomPrefab(GameObject[] GoFabs)
    {
        int GoIndex = Random.Range(0, GoFabs.Length);
        return GoFabs [GoIndex]  ;
    }
    }
	

