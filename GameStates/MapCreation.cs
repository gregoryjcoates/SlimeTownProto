using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    // need to set a limiter for x, y, and texture
    public int x = 200;
    public int y = 200;
    public int x2 = 201;
    public int y2 = 201;
    public int x3 = 400;
    public int y3 = 400;
    public int texture1 = 0;
    public int texture2 = 1;
    public int texture3 = 2;
    public float alpha = 0.5f;
    public Terrain mainTerrain;
    // set sizes for each biome area and generate biomes. 
    private void Start()
    {
        

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            PaintTerrain();
        }
    }

    void PaintTerrain()
    {
        // SplatPrototype Map = new SplatPrototype();
       //  Map.texture = (Texture2D)Resources.Load("Terrain/grass");
       //  Map.tileOffset = new Vector2(0, 0);
       //  Map.tileSize = new Vector2(10, 10);


        // float[,,] map = float[0, 0, 0]; 

        
        float[,,] map = new float[mainTerrain.terrainData.alphamapWidth,mainTerrain.terrainData.alphamapHeight, 3];
        for (int i = 0; i < x; i++)
        {
            for (int i2 = 0;i2 < y;i2++)
            {
                map[i,i2, texture1] = alpha;
               // for blending textures map[i, i2, texture2] = .4f;
            }
        }

        for (int i = x2; i < x3; i++)
        {
            for (int i2 = y2; i2 < y3; i2++)
            {
                map[i, i2, texture2] = alpha;
                // for blending textures map[i, i2, texture2] = .4f;
            }
        }

        for (int i = x3; i < 512; i++)
        {
            for (int i2 = y3; i2 < 512; i2++)
            {
                map[i, i2, texture3] = alpha;
                // for blending textures map[i, i2, texture2] = .4f;
            }
        }


        // paint roads
        for (int i = 0; i<512;i++)
        {
            map[50, i, texture2] = 0;
        }
        //map[a, b, c] = d;
        mainTerrain.terrainData.SetAlphamaps(0, 0, map);
    }
}
