using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField]
    public int _seed = 69420;
    public List<ChunkAllegiance> biomeChunks = new List<ChunkAllegiance>();
    public GameObject exitChunk, startChunk, nullChunk;
    public List<GameObject> placedChunks = new List<GameObject>();
    private List<MazeId> _mazeList = new List<MazeId>();
    public PlayerSpawnpointManager _psm;
    [Tooltip("Which maze template to generate")]
    public int generateMazeId = 0;
    [Header("Y must have consistent numbers")]
    [Tooltip("Consider this a cordinate system, 0 means no change, 1 means removed, 2 means spawnpoint, 3 means end")]
    public List<MazeTemplate> mazeTemplates;
    public GameObject key;
    private List<Transform> _plainChunk = new List<Transform>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        //GenerateMaze();
        GenerateMaze();
    }
    void GenerateMaze(){
        // Set seed for random gen
        Random.InitState(_seed);

        int mazeWidth = mazeTemplates[generateMazeId].x.Count;
        int mazeLength = mazeTemplates[generateMazeId].x[0].y.Count;
        int spawnedPoints = 0;
        
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
                        if(biomeId == 2){
                            _plainChunk.Add(g.transform);
                        }
                    break;
                    case 1: // Creates an empty chunk
                        g = Instantiate(nullChunk);
                    break;
                    case 2: // Creates a spawnpoint chunk
                        g = Instantiate(startChunk);
                        g.GetComponentInChildren<PlayerSpawnpoint>().spawnpointId = spawnedPoints;
                        spawnedPoints++;
                    break;
                    case 3: // Creates an exit chunk
                        g = Instantiate(exitChunk);
                    break;
                    
                }
                placedChunks.Add(g);
                g.transform.parent = transform;
                g.transform.position = new Vector3(w * 20, 2.5f, l * 20);
                _mazeList.Add(g.GetComponent<MazeId>());
                _mazeList[_mazeList.Count - 1].placeLocation = g.transform.position;
            }
        }
        if(_plainChunk.Count > 0){  // Creates a key
            if(key != null){
                Instantiate(key, _plainChunk[Random.Range(0, _plainChunk.Count)].position, Quaternion.identity);
            }
        }
        _psm._spawnPoints.AddRange(FindObjectsOfType<PlayerSpawnpoint>());
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
                        if(Vector3.Distance(item.transform.position,wall.transform.position) < 0.6f){
                            for (int i = 0; i < wall.transform.childCount; i++)
                            {
                                wall.transform.GetChild(i).gameObject.SetActive(false);
                                Destroy(wall.transform.GetChild(i).GetComponentInChildren<MeshFilter>());
                            }
                            for (int i = 0; i < item.transform.childCount; i++)
                            {
                                item.transform.GetChild(i).gameObject.SetActive(false);
                                Destroy(item.transform.GetChild(i).GetComponentInChildren<MeshFilter>());
                            }
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
        foreach (var item in walls){
            foreach (var wall in walls){
                for (int i = 0; i < wall.transform.childCount; i++)
                {
                    wall.transform.GetChild(i).parent = transform;
                }               
                for (int i = 0; i < item.transform.childCount; i++)
                {
                    item.transform.GetChild(i).parent = transform;
                }

            }
        }
        foreach (var item in placedChunks)
        {
            item.transform.position = Vector3.zero;
        }
        foreach (var item in _mazeList)
        {
            item.CheckArchways();
        }

        // moves the players to their respective spawnpoints
        _psm.Invoke("SpawnPlayers", 0.1f);
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
