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

    
    
    // set sizes for each biome area and generate biomes. 


    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            PaintTerrain();
        }
    }

    public void PaintTerrain()
    {


        
        float[,,] map = new float[mainTerrain.terrainData.alphamapWidth,mainTerrain.terrainData.alphamapHeight, 3];
        int tHeight = mainTerrain.terrainData.alphamapHeight / 2;
        int tWidth = mainTerrain.terrainData.alphamapWidth / 2;

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
}
