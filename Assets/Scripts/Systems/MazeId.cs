using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeId : MonoBehaviour
{
    public List<MazeDoorIndicator> wallDoors;
    public Vector3 placeLocation;
    public GameObject archway;
    public List<GameObject> _archways = new List<GameObject>();

    void Awake(){
        // add archway to every wall door
        if(archway != null){

            foreach (var item in wallDoors)
            {
                GameObject arch = Instantiate(archway, item.transform.position, Quaternion.identity);
                _archways.Add(arch);
                arch.transform.parent = transform;
                switch (item.gameObject.name.ToString())
                {
                    case "R":
                        for (int i = 0; i < arch.transform.childCount; i++)
                        {
                            arch.transform.GetChild(i).transform.position += new Vector3(0,0,0.5f);
                        }
                    break;
                    case "L":
                        for (int i = 0; i < arch.transform.childCount; i++)
                        {
                            arch.transform.GetChild(i).transform.position += new Vector3(0,0,-0.5f);
                        }
                    break;
                    case "B":
                        arch.transform.Rotate(new Vector3(0,90,0));
                        for (int i = 0; i < arch.transform.childCount; i++)
                        {
                            arch.transform.GetChild(i).transform.position += new Vector3(0.5f,0,0);
                        }
                    break;
                    case "T":
                        arch.transform.Rotate(new Vector3(0,90,0));
                        for (int i = 0; i < arch.transform.childCount; i++)
                        {
                            arch.transform.GetChild(i).transform.position += new Vector3(-0.5f,0,0);
                        }
                    break;
                }
            }
        }
    }
    public void CheckArchways(){
        Debug.Log(_archways.Count);
        foreach (var item in wallDoors)
        {
            if(item.gameObject.activeInHierarchy){
                foreach (var arch in _archways)
                {
                    if(item.transform.position == arch.transform.position){
                        Destroy(arch.gameObject);
                        arch.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}