using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    // need to set a limiter for x, y, and texture
    //public int x = 200;
    //public int y = 200;
    //public int x2 = 201;
    //public int y2 = 201;
    //public int x3 = 400;
    //public int y3 = 400;

    
    public int texture1BottomLeft = 0;
    public int texture2TopLeft = 0;
    public int texture3BottomRight = 0;
    public int texture4TopRight = 0;
    public float alpha = 0.5f;
    public float alpha2 = 0.5f;
    public float alpha3 = 0.5f;
    public float alpha4 = 0.5f;


   // public float mapGridSize = 1f;
    public Terrain mainTerrain;
    public GameObject WarpPoint;
    public GameObject SlimeHome;
    public GameObject testobject;

    public int houseCount = 30;
    // set sizes for each biome area and generate biomes. 


    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            PaintTerrain();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
         //   PlaceWarpPoints();
            MapCreationV2(houseCount);
        }
    }

    public void PaintTerrain()
    {
        int tHeight = AlphaMapSize().tHeight;
        int tWidth = AlphaMapSize().tWidth;
        float[,,] map = AlphaMapSize().map;


        // Quadrant Creation and painting
        // create quadrants
        // q24
        // q13
        // quarant 1 bottom left

        for (int i = 0; i < tHeight;i++)
        {
            for (int i2 = 0; i2 < tWidth; i2++)
            {
                map[i, i2, texture1BottomLeft] = alpha;
            }
        }

        //quadrant 2 top left

        for (int i = tHeight; i < tHeight*2; i++)
        {
            for(int i2 = 0; i2 < tWidth; i2++)
            {
                map[i, i2, texture2TopLeft] = alpha2;
            }
        }    
        
        //quadrant 3 bottom right

        for (int i = 0; i < tHeight; i++)
        {
            for (int i2 = tWidth; i2 < tWidth*2; i2++)
            {
                map[i, i2, texture3BottomRight] = alpha3;
            }
        }
        
        //quadrant 4 top right

        for (int i = tHeight; i < tHeight*2; i++)
        {
            for (int i2 = tWidth; i2 < tWidth*2; i2++)
            {
                map[i, i2, texture4TopRight] = alpha4;
            }
        }

        mainTerrain.terrainData.SetAlphamaps(0, 0, map);
    }

    void MapSections()
    {
        // code for creating subsections and zones of the map
    }

    public void PlaceWarpPoints()
    {


        int numberWarpSets = 5;

        float mapHeight = MapSize().mapHeight;
        float mapWidth = MapSize().mapWidth;

        // early code for initializing and placing warp points

        for (int i = 0; i < numberWarpSets ;i++)
        {

            //  prelimiary code for placing warp points on the map
            float x = Random.Range(10, mapWidth - 10);
            float z = Random.Range(10, mapHeight -10);

            float x2 = Random.Range(10, mapWidth - 10);
            float z2 = Random.Range(10, mapHeight - 10);


            Vector3 warpLocationOne = new Vector3(x, 0, z);
            Vector3 warpLocationTwo = new Vector3(x2, 0, z2);

            GameObject warpPointOne = Instantiate(WarpPoint, warpLocationOne, Quaternion.identity);
            GameObject warpPointTwo = Instantiate(WarpPoint, warpLocationTwo, Quaternion.identity);
            
            WarpSystem warpTargetOne = warpPointOne.GetComponent<WarpSystem>();
            WarpSystem warptargetTwo = warpPointTwo.GetComponent<WarpSystem>();

            warpTargetOne.warpTarget = warpLocationTwo - new Vector3(0,0,3);
            warptargetTwo.warpTarget = warpLocationOne - new Vector3(0,0,3);


        }

    }


    // gets the terrain data for the gameobject this script is attached to, specifically the width and height. returns all 3
    public (int tHeight,int tWidth,float[,,] map) AlphaMapSize()
    {
        float[,,] map = new float[mainTerrain.terrainData.alphamapWidth, mainTerrain.terrainData.alphamapHeight, 3];
        int tHeight = mainTerrain.terrainData.alphamapHeight / 2;
        int tWidth = mainTerrain.terrainData.alphamapWidth / 2;
        return (tHeight,tWidth,map);
    }

    // map creation v2
    void MapCreationV2(int count)
    {
        float tHeight = MapSize().mapHeight-10;
        float tWidth = MapSize().mapWidth-10;
        int failed = 0;
        for (int i = 0; i < count; i++)
        {
            float a = Random.Range(0, tHeight);
            float b = Random.Range(0, tWidth);
            float y = mainTerrain.terrainData.GetHeight(System.Convert.ToInt32(tWidth), System.Convert.ToInt32(tHeight));

            // need distance to bottom of object being placed;

            Vector3 placeHome = new Vector3(a, y, b);
            // float checkHeight = SlimeHome.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y;
            float checkHeight = SlimeHome.GetComponent<Renderer>().bounds.size.y/2;
            Debug.Log(checkHeight);
            Debug.Log("this is the location"+placeHome);

            Vector3 adjustedHeight = new Vector3(0,checkHeight+placeHome.y,0);
            placeHome += adjustedHeight;

            if (CheckOverlap(placeHome, SlimeHome) == false)
            {
                GameObject slimeHouse = Instantiate(SlimeHome, placeHome, Quaternion.identity);
                failed = 0;
            }
            else 
            {
                // if placement fails x amount of times breaks out of loop
                if (failed == 10)
                {
                    Debug.LogError("no room to palce object");
                    break;
                }
                else
                {
                    failed++;
                    i--;
                }

            }

        }

    }

    bool CheckOverlap(Vector3 location, GameObject thingy)
    {
        Vector3 scaleIncrease = new Vector3(1, 0, 1);
        Collider[] hits = Physics.OverlapBox(location, thingy.transform.localScale, Quaternion.identity, 1<<8);
        if (hits.Length > 0)
        {
            Debug.Log(" I got overlap!");
            return true;
        }
        else
        {
            return false;
        }
    }

    (float mapWidth , float mapHeight ) MapSize()
    {
        float mapWidth = mainTerrain.terrainData.size.x;
        float mapHeight = mainTerrain.terrainData.size.z;
        return (mapWidth, mapHeight);
    }

}