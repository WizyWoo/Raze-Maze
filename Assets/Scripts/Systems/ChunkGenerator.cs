using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    public int mazeLength = 3;
    public List<GameObject> chunks = new List<GameObject>();
    public List<GameObject> placedChunks = new List<GameObject>();
    private Transform _chunkStart, _chunkEnd;
    public LayerMask layerMask = 32768;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateMaze();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateMaze(){
        for (int i = 0; i < mazeLength; i++)
        {
            int r = Random.Range(0, chunks.Count);
            GameObject g = Instantiate(chunks[r]);
            placedChunks.Add(g);
            _chunkStart = g.GetComponent<MazeId>().chunkStart;
            _chunkStart.transform.parent = transform;
            g.transform.parent = _chunkStart;
            if(_chunkEnd == null){
                // Move maze to 0
                _chunkStart.position = transform.position;
                _chunkStart.rotation = transform.rotation;
            }else{
                // Move maze to last chunk end
                _chunkStart.position = _chunkEnd.position;
                _chunkStart.rotation = _chunkEnd.rotation;
                //placedChunks[placedChunks.Count - 2].GetComponent<MazeId>().boxCollider
            }
            _chunkStart.localScale = new Vector3((Random.Range(0,2) * 2 - 1),1,1);
            BoxCollider box = g.GetComponent<MazeId>().boxCollider;
            Collider[] hitColliders = Physics.OverlapBox(box.center + box.transform.position, box.size, box.transform.rotation, layerMask);
            if(hitColliders.Length > 0){
                foreach (var item in hitColliders)
                {
                    print(item.transform.position);
                }
            }
            g.transform.parent = transform;
            _chunkStart.transform.parent = g.transform;
            g.transform.parent = transform;

            
            _chunkEnd = g.GetComponent<MazeId>().chunkEnd;
        }
            
    }
}
