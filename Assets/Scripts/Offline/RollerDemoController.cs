using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RollerDemoController : MonoBehaviour
{
    public TMP_InputField mSeed, mId;
    public GameObject minimap, overview;
    public ChunkGenerator cG;
    void Awake()
    {
        if(PlayerPrefs.HasKey("mazeSeed")){
            mSeed.text =            PlayerPrefs.GetInt("mazeSeed").ToString();
            cG._seed =              PlayerPrefs.GetInt("mazeSeed");
        }
        if(PlayerPrefs.HasKey("mazeId")){
            mId.text =              PlayerPrefs.GetInt("mazeId").ToString();
            cG.generateMazeId =     PlayerPrefs.GetInt("mazeId");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            overview.SetActive(!overview.activeInHierarchy);
            minimap.SetActive(!minimap.activeInHierarchy);
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
    public void SeedChange(){
        PlayerPrefs.SetInt("mazeSeed", int.Parse(mSeed.text));
    }
    public void IdChange(){
        PlayerPrefs.SetInt("mazeId", int.Parse(mId.text));
    }
    public void RegenerateMaze(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
