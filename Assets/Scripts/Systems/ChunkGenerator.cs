using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField]
    private int _seed = 69420;
    public List<ChunkAllegiance> biomeChunks = new List<ChunkAllegiance>();
    public GameObject exitChunk, startChunk, nullChunk;
    public List<GameObject> placedChunks = new List<GameObject>();
    private List<MazeId> _mazeList = new List<MazeId>();
    private PlayerSpawnpointManager _psm;
    [Tooltip("Which maze template to generate")]
    public int generateMazeId = 0;
    [Header("Y must have consistent numbers")]
    [Tooltip("Consider this a cordinate system, 0 means no change, 1 means removed, 2 means spawnpoint, 3 means end")]
    public List<MazeTemplate> mazeTemplates;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _psm = GetComponent<PlayerSpawnpointManager>();
        //GenerateMaze();
        GenerateMaze();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateMaze(){
        // Set seed for random gen
        Random.InitState(_seed);

        int mazeWidth = mazeTemplates[generateMazeId].x.Count;
        int mazeLength = mazeTemplates[generateMazeId].x[0].y.Count;
        
        for (int w = 0; w < mazeWidth; w++)
        {
            for (int l = 0; l < mazeLength; l++)
            {
                GameObject g = nullChunk;
                switch (mazeTemplates[generateMazeId].x[w].y[l])
                {
                    case 0: // Creates chunk as normal
                        int biomeId = mazeTemplates[generateMazeId].x[w].biome[l];
                        int r = Random.Range(0, biomeChunks[biomeId].chunks.Count);
                        g = Instantiate(biomeChunks[biomeId].chunks[r]);
                    break;
                    case 1: // Creates an empty chunk
                        g = Instantiate(nullChunk);
                    break;
                    case 2:
                        g = Instantiate(startChunk);
                    break;
                    case 3:
                        g = Instantiate(exitChunk);
                    break;
                    
                }
                placedChunks.Add(g);
                g.transform.parent = transform;
                g.transform.position = new Vector3(w * 20, 2.5f, l * 20);
                _mazeList.Add(g.GetComponent<MazeId>());
            }
        }
        CheckChunkConnection();
        }
    void CheckChunkConnection(){
        // Go through all the maze parts
        List<MazeDoorIndicator> walls = new List<MazeDoorIndicator>();
        foreach (var item in _mazeList)
        {
            walls.AddRange(item.wallDoors);
        }

        foreach (var item in walls)
        {
            bool hasMatch = false;
            foreach (var wall in walls)
            {
                if(wall.gameObject != item.gameObject){
                    if(!item.gameObject.activeInHierarchy || !wall.gameObject.activeInHierarchy){
                        if(Vector3.Distance(item.transform.position,wall.transform.position) < 0.1f){
                            wall.gameObject.SetActive(false);
                            item.gameObject.SetActive(false);
                            Destroy(wall.GetComponent<MeshFilter>());
                            Destroy(item.GetComponent<MeshFilter>());

                            hasMatch = true;
                        }
                    }
                }
            }
            if(!hasMatch){
                item.gameObject.SetActive(true);
            }
        }
        // moves the players to their respective spawnpoints
        _psm.SpawnPlayers();
    }
    
}
[System.Serializable]
public class MazeTemplate{
    public List<HeightList> x;
}
[System.Serializable]
public class HeightList{
    public List<int> y;
    [Tooltip("biome length must equal Y")]
    public List<int> biome;

}
[System.Serializable]
public class ChunkAllegiance{
    public List<GameObject> chunks;
}
