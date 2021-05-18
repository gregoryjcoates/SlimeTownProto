using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreationV2 : MonoBehaviour
{
    // version two of my map creation script implenting complete procedural generation

    // terrain
    public float alpha = 0.5f;
    // GameObjects
    [SerializeField]
    Terrain mainTerrain;
    [SerializeField]
    GameObject slimeHome;
    [SerializeField]
    GameObject warpPoint;
    [SerializeField]
    GameObject tree1;
    [SerializeField]
    GameObject lake;
    [SerializeField] 
    GameObject ruins;
    [SerializeField]
    GameObject enemy1;

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


    // values for quadrant creation
    bool slimeHomePlaced = false;
    // Values for enemy creation
    [SerializeField]
    int numberOfEnemies = 100;
    [SerializeField]
    int numberOfTraps = 1;

    Dictionary<int, List<Vector3>> enemyLocations = new Dictionary<int, List<Vector3>>();

    // Forest
    bool lakePlaced = false;
    [SerializeField]
    int numberOfLakes = 1;
    bool ruinsPlaced = false;
    [SerializeField]
    int numberOfRuins = 1;


    // for warp point placement
    Dictionary<string, Vector3> specialLocationCoordinates = new Dictionary<string, Vector3>();
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
        // set number of traps to at least number of enemies
        numberOfTraps += numberOfEnemies;

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
        int numberOfTrees = ((int)width*(int)height)/48;
      //  float x = Random.Range(w, width);
       // float y = Random.Range(h, height);


        //overlap box is not the right size???? trees should be randomly rotated
        if (quadType == forest)
        {
            GameObject treeParent = new GameObject();
            treeParent.name = "Tree Parent";

            GameObject enemyParent = new GameObject();
            enemyParent.name = "Enemy Parent";

            // place special areas

            //Place the slimeHome
            if (slimeHomePlaced == false)
            {
                Vector3 checkHeight = slimeHome.GetComponent<Renderer>().bounds.size / 2;
                float x = Random.Range(w + checkHeight.x + 10f, width - checkHeight.x - 10f);
                float z = Random.Range(h + checkHeight.z + 10f, height - checkHeight.z - 10f);
                float y = mainTerrain.terrainData.GetHeight((int)x, (int)z);
                Vector3 placementLocation = new Vector3(x, y, z);


                //convert to vector3 to get both x and y to keep object within map bounds
                // float checkHeight = slimeHome.GetComponent<Renderer>().bounds.size.y / 2;




                placementLocation.y += checkHeight.y;

                GameObject SlimeBase = Instantiate(slimeHome, placementLocation, Quaternion.identity);

                //  AddSpecialLocationsToArray(placementLocation);

                specialLocationCoordinates.Add("slimehome", placementLocation);
                slimeHomePlaced = true;


            }

            // Place lake
            if (lakePlaced == false)
            {
                for (int i = 0; i < numberOfLakes; i++)
                {
                    Vector3 checkHeight = lake.GetComponent<Renderer>().bounds.size / 2;
                    float x = Random.Range(w + checkHeight.x + 10f, width - checkHeight.x - 10f);
                    float z = Random.Range(h + checkHeight.z + 10f, height - checkHeight.z - 10f);
                    float y = mainTerrain.terrainData.GetHeight((int)x, (int)z);

                    Vector3 placementLocation = new Vector3(x, y, z);

                    placementLocation.y += checkHeight.y;

                    
                    if (CheckOverlap(placementLocation,lake) == false)
                    {
                        GameObject Lake = Instantiate(lake, placementLocation, Quaternion.identity);
                        //AddSpecialLocationsToArray(placementLocation);
                        specialLocationCoordinates.Add("lake" + i, placementLocation);
                    }
                    else
                    {
                        i--;
                    }
                }
                lakePlaced = true;
            }

            // Place ruins
            if (ruinsPlaced == false)
            {
                for (int i = 0; i < numberOfRuins; i++)
                {
                    Vector3 checkHeight = ruins.GetComponent<Renderer>().bounds.size / 2;

                    float x = Random.Range(w + checkHeight.x + 10f, width - checkHeight.x - 10f);
                    float z = Random.Range(h + checkHeight.z + 10f, height - checkHeight.z - 10f);

                    float y = mainTerrain.terrainData.GetHeight((int)x, (int)z);

                    Vector3 placementLocation = new Vector3(x, y, z);


                    placementLocation.y += checkHeight.y;

                    if (CheckOverlap(placementLocation, lake) == false)
                    {
                        GameObject Ruins = Instantiate(ruins, placementLocation, Quaternion.identity);
                        //  AddSpecialLocationsToArray(placementLocation);
                        specialLocationCoordinates.Add("ruins" + i, placementLocation);
                    }
                    else
                    {
                        i--;
                    }

                }
                ruinsPlaced = true;
            }

            // Place trees
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

                    if (x > 0 & x < width)
                    {
                        if (z > 0 & z < height)
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

            List<GameObject> enemyList = new List<GameObject>();

            enemyList.Add(enemy1);

            int maxEnemies = 10+1;

            for (int i = 0; i < maxEnemies; i++)
            {
                enemyList.Add(enemy1);
            }

            for (int i = 0; i < enemyList.Count-1; i++)
            {
                enemyLocations.Add(i,new List<Vector3>());
            }
            Debug.Log(enemyList.Count);
            int spawnThisEnemy = 0;
            // place enemies
            for (int i = 0; i < numberOfEnemies; i++)
            {
                float x = Random.Range(w, width);
                float z = Random.Range(h, height);
                float y = mainTerrain.terrainData.GetHeight((int)x, (int)z);



                if (x > 0 & x < width & z > 0 & z < height)
                {
                    Vector3 checkHeight = ruins.GetComponent<Renderer>().bounds.size / 2;

                    Vector3 placementLocation = new Vector3(x, y, z);
                    placementLocation.y += checkHeight.y;

                    // scaling system for placing enemies
                    float distanceFromBase = Vector3.Distance(specialLocationCoordinates["slimehome"],placementLocation);

                    // gives a number between 0.0 and 1.0 turning distance from slime home into a decimal
                    float difficultyScale = Mathf.InverseLerp(0f, MapSize().mapWidth, distanceFromBase);


                    // the heigher difficulty scale is the stronger the enemies are
                    if (Random.value <= difficultyScale)
                    {
                        spawnThisEnemy = Random.Range(0, maxEnemies);

                        if (Random.value > 0.8f)
                        {
                            spawnThisEnemy = Random.Range(8, maxEnemies);
                        }
                    }
                    else
                    {
                        spawnThisEnemy = Random.Range(0, maxEnemies-3);
                    }


                    if (CheckOverlap(placementLocation,enemyList[spawnThisEnemy]))
                    {
                        GameObject enemy = Instantiate(enemyList[spawnThisEnemy], placementLocation, Quaternion.identity);
                        enemy.transform.parent = enemyParent.transform;
                        List<Vector3> a = enemyLocations[spawnThisEnemy];
                        a.Add(placementLocation);

                    }
                    else
                    {
                        i--;
                    }
                }
            }
            // place traps
            for (int i = 0; i < numberOfEnemies; i++)
            {

            }
            // place warp points


                int numberWarpSets = 5;

                float mapHeight = MapSize().mapHeight;
                float mapWidth = MapSize().mapWidth;

                // early code for initializing and placing warp points

                for (int i = 0; i < numberWarpSets; i++)
                {

                    //  prelimiary code for placing warp points on the map
                    float x = Random.Range(10, mapWidth - 10);
                    float z = Random.Range(10, mapHeight - 10);

                    float x2 = Random.Range(10, mapWidth - 10);
                    float z2 = Random.Range(10, mapHeight - 10);


                    Vector3 warpLocationOne = new Vector3(x, 0, z);
                    Vector3 warpLocationTwo = new Vector3(x2, 0, z2);

                    GameObject warpPointOne = Instantiate(warpPoint, warpLocationOne, Quaternion.identity);
                    GameObject warpPointTwo = Instantiate(warpPoint, warpLocationTwo, Quaternion.identity);

                    WarpSystem warpTargetOne = warpPointOne.GetComponent<WarpSystem>();
                    WarpSystem warptargetTwo = warpPointTwo.GetComponent<WarpSystem>();

                    warpTargetOne.warpTarget = warpLocationTwo - new Vector3(0, 0, 3);
                    warptargetTwo.warpTarget = warpLocationOne - new Vector3(0, 0, 3);


                }

            for (int i = 0; i < enemyLocations.Count; i++)
            {
                Debug.Log("this is the size/count of enemyLocations "+enemyLocations.Count);
                List<Vector3> a = enemyLocations[i];
                
                for (int l = 0; l < a.Count; l++)
                {
                    Debug.Log(a[l]);
                }
            }
            
        }

        if (quadType == desert)
        {

        }

        if (quadType == village)
        {

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
