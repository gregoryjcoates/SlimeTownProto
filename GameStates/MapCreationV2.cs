using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreationV2 : MonoBehaviour
{
    // version two of my map creation script implenting complete procedural generation

    // terrain
    public float alpha = 0.5f;
    // GameObjects
    public Terrain mainTerrain;
    public GameObject slimeHome;
    public GameObject tree1;

    //QuadrantTypes
    enum QuadrantType
    {
        Forest = 0,
        Desert = 2,
        Village = 0
    }

    int forest = (int) QuadrantType.Forest;
    int desert = (int) QuadrantType.Desert;
    int village = (int) QuadrantType.Village;
    // the main function

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            MapCreation();
        } 
    }
    void MapCreation()
    {
        // gets the amount of textures currently aded to the terrain 
        AlphaMapQuadrants();

        // breaks the map into 4 quadrants
        MapQuadrants();
    }




    // get alphamap size
    (int tHeight, int tWidth, float[,,] map) AlphaMapSize()
    {
        float[,,] map = new float[mainTerrain.terrainData.alphamapWidth, mainTerrain.terrainData.alphamapHeight, 3];
        int tHeight = mainTerrain.terrainData.alphamapHeight;
        int tWidth = mainTerrain.terrainData.alphamapWidth;
        return (tHeight, tWidth, map);
    }

    // get World Coordinate size
    (float mapWidth, float mapHeight) MapSize()
    {
        float mapWidth =  mainTerrain.terrainData.size.x;
        float mapHeight = mainTerrain.terrainData.size.z;
        return (mapWidth, mapHeight);
    }


    // make quadrants
    void MapQuadrants()
    {
        float width = MapSize().mapWidth;
        float height = MapSize().mapHeight;

        //bottom left
        CreateQuadrant(width/2 , height/2 ,0 , 0,forest);

        //bottom right
       // float[,] quadTwo =CreateQuadrant(width , height/2 , System.Convert.ToInt32(width)/2 , 0);

        //top left
       // float[,] quadThree = CreateQuadrant(width/2 , height , 0 ,System.Convert.ToInt32(height)/2);

        //top right
        //float[,] quadFour = CreateQuadrant(width, height, System.Convert.ToInt32(width)/2, System.Convert.ToInt32(height)/2);

       
    }


    // make alphamap quadrants
    void AlphaMapQuadrants()
    {

        int tHeight = AlphaMapSize().tHeight;
        int tWidth = AlphaMapSize().tWidth;
        float[,,] map = AlphaMapSize().map;


        //bottom left
        CreateAlphaMapQuadrant(tWidth / 2, tHeight / 2, 0, 0,map,forest);
        //bottom right
        CreateAlphaMapQuadrant(tWidth, tHeight / 2, tWidth / 2, 0,map, desert);

        //top left
        CreateAlphaMapQuadrant(tWidth / 2, tHeight, 0, tHeight / 2,map, desert);

        //top right
        CreateAlphaMapQuadrant(tWidth, tHeight, tWidth / 2, tHeight / 2,map, village);

        mainTerrain.terrainData.SetAlphamaps(0, 0, map);
    }

    //float[,] CreateQuadrant(float width, float height,int w, int h)
    //{
    //    float[,] quadrantCoordinates = new float[System.Convert.ToInt32(width),System.Convert.ToInt32(height)];

    //    for ( int i = w ; i < quadrantCoordinates.GetLength(0); i++)
    //    {
    //        for ( int i2 = h ; i2 < quadrantCoordinates.GetLength(1); i2++)
    //        {

    //        }
    //    }
    //    return quadrantCoordinates;
    //}

    void CreateQuadrant(float width, float height, int w, int h,int quadType)
    {
        int numberOfTrees = ((int)width*(int)height)/40;
      //  float x = Random.Range(w, width);
       // float y = Random.Range(h, height);


        //overlap box is not the right size???? trees should be randomly rotated
        if (quadType == forest)
        {
            GameObject treeParent = new GameObject();
            treeParent.name = "Tree Parent";

            for (int i = 0; i < numberOfTrees; i++)
            {
                //sets the number of trees grouped together
                int treeGroupSize = Random.Range(1,8);
                //sets the x and z coordinates of the tree
                float x = Random.Range(w, width);
                float z = Random.Range(h, height);

                //places the trees in a group and screws the number slightly to spread them out just a bit
                for (int a = 0; a < treeGroupSize; a++)
                {
                    //screws tree placement
                    x += Random.Range(-1.5f, 1.5f);
                    z += Random.Range(-1.5f, 1.5f);

                    //uses tree location and gets the height of the terrain at that location to ensure proper object height
                    float y = mainTerrain.terrainData.GetHeight((int)x, (int)z);

                    if (x > 0 & x < MapSize().mapWidth)
                    {
                        if (z > 0 & z < MapSize().mapHeight)
                        {

                            Vector3 placementLocation = new Vector3(x, y, z);


                            if (CheckOverlap(placementLocation, tree1) == false)
                            {
                                GameObject tree = Instantiate(tree1, placementLocation, transform.rotation * Quaternion.Euler(0, Random.Range(0, 359), 0));
                                tree.transform.parent = treeParent.transform;
                            }
                        }
                        else
                        {
                            a--;
                        }
                    }

                    

                }
          
            }
        }
    }

    float[,,] CreateAlphaMapQuadrant(int width, int height, int w, int h, float[,,] map, int a )
    {

          for (int i = w ; i < width; i++)
          {
              for (int i2 = h; i2 < height; i2++)
           {
               map[i, i2, a] = alpha;
            }
         }

        return map;
    }

    bool CheckOverlap(Vector3 location, GameObject thingy)
    {

        Collider[] hits = Physics.OverlapBox(location, thingy.transform.localScale, Quaternion.identity, 1 << 8);
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
}
