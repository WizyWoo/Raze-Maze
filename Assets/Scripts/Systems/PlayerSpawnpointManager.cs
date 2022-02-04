using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnpointManager : MonoBehaviour
{
    private List<PlayerSpawnpoint> _spawnPoints = new List<PlayerSpawnpoint>();
    private List<PlayerController> _players = new List<PlayerController>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnPlayers(){
        _spawnPoints.AddRange(FindObjectsOfType<PlayerSpawnpoint>());
        _players.AddRange(FindObjectsOfType<PlayerController>());

        for (int i = 0; i < _players.Count; i++)
        {
            _players[i].transform.position = _spawnPoints[i].transform.position;
        }
    }
}
