using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawnpointManager : MonoBehaviour
{

    [HideInInspector]
    public List<PlayerSpawnpoint> _spawnPoints = new List<PlayerSpawnpoint>();
    [HideInInspector]
    public List<Com.MyCompany.MyGame.PlayerManager> _players = new List<Com.MyCompany.MyGame.PlayerManager>();
    private bool scanning;
    private int storedCount;
    private int playerId;
    
    void Awake(){
        this.transform.parent = null;

    }
    private void Start() {
        playerId = PhotonNetwork.LocalPlayer.ActorNumber;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        storedCount = _players.Count;
        if(scanning){
            List<Com.MyCompany.MyGame.PlayerManager> pl = new List<Com.MyCompany.MyGame.PlayerManager>();
            pl.AddRange(FindObjectsOfType<Com.MyCompany.MyGame.PlayerManager>());
            foreach (var item in pl)
            {
                bool isFound = false;
                foreach (var player in _players)
                {
                    if(player == item){
                        isFound = true;
                    }

                }
                if(!isFound){
                    _players.Add(item);
                    print("hi");
                }
            }
        }
        if(_players.Count > storedCount){   // Needs testing
            _players[_players.Count - 1].transform.position = _spawnPoints[playerId % (_spawnPoints.Count - 1)].transform.position;
        }
    }
    public void SpawnPlayers(){
        scanning = true;
        /*_players.AddRange(FindObjectsOfType<Com.MyCompany.MyGame.PlayerManager>());

        for (int i = 0; i < _players.Count; i++)
        {
            _players[i].transform.position = _spawnPoints[i].transform.position;
        }*/
    }
}
