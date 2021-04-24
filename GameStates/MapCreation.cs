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
    public int texture3BottomRight = 00;
    public int texture4TopRight = 0;
    public float alpha = 0.5f;
    public float alpha2 = 0.5f;
    public float alpha3 = 0.5f;
    public float alpha4 = 0.5f;
    public Terrain mainTerrain;



    public GameObject WarpPoint;
    
    // set sizes for each biome area and generate biomes. 


    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            PaintTerrain();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            PlaceWarpPoints();
        }
    }

    public void PaintTerrain()
    {

        int tHeight = MapSize().tHeight;
        int tWidth = MapSize().tWidth;
        float[,,] map = MapSize().map;
        
       // float[,,] map = new float[mainTerrain.terrainData.alphamapWidth,mainTerrain.terrainData.alphamapHeight, 3];
      //  int tHeight = mainTerrain.terrainData.alphamapHeight / 2;
      //  int tWidth = mainTerrain.terrainData.alphamapWidth / 2;

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

        //causes a black line through the map??
        // for (int i = 0; i<512;i++)
        // {
        //     map[50, i, texture2TopLeft] = 0;
        // }
        mainTerrain.terrainData.SetAlphamaps(0, 0, map);
    }

    void MapSections()
    {
        // code for creating subsections and zones of the map
    }

    public void PlaceWarpPoints()
    {


        int numberWarpSets = 5;

        int mapHeight = MapSize().tHeight;
        int mapWidth = MapSize().tWidth;

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

            Debug.Log(warpLocationOne);
            Debug.Log(warpLocationTwo);

        }

    }


    // gets the terrain data for the gameobject this script is attached to, specifically the width and height. returns all 3
    public (int tHeight,int tWidth,float[,,] map) MapSize()
    {
        float[,,] map = new float[mainTerrain.terrainData.alphamapWidth, mainTerrain.terrainData.alphamapHeight, 3];
        int tHeight = mainTerrain.terrainData.alphamapHeight / 2;
        int tWidth = mainTerrain.terrainData.alphamapWidth / 2;

        return (tHeight,tWidth,map);
    }

}
