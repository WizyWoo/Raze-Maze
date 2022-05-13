using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeId : MonoBehaviour
{
    public List<MazeDoorIndicator> wallDoors;
    public Vector3 placeLocation;
    public GameObject archway;

    void Start(){
        // add archway to every wall door
        if(archway != null){

            foreach (var item in wallDoors)
            {
                GameObject arch = Instantiate(archway, item.transform.position, Quaternion.identity);
                arch.transform.parent = transform.GetChild(0).transform;
                switch (item.gameObject.name.ToString())
                {
                    case "R":
                    break;
                    case "L":
                    break;
                    case "B":
                        Debug.Log("B");
                        arch.transform.Rotate(new Vector3(0,90,0));
                    break;
                    case "T":
                        Debug.Log("T");
                        arch.transform.Rotate(new Vector3(0,90,0));
                    break;
                }
            }
        }
    }
}