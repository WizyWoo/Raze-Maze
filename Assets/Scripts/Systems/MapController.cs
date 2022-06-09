using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Transform trackingPoint, startPoint;
    private Transform _playerPencil;
    private int _mapWidth = 512;
    // Start is called before the first frame update
    void Start()
    {
        _playerPencil = Camera.main.transform;
        StartCoroutine(Drawer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Drawer(){
        yield return new WaitForSeconds(0.65f);
        Texture2D texture = new Texture2D(_mapWidth, _mapWidth);
        GetComponent<Renderer>().material.mainTexture = texture;
        Vector3 width = transform.lossyScale;
        startPoint.position = trackingPoint.position = new Vector3(
                transform.position.x + ((_playerPencil.position.x - 64) / _mapWidth), 
                transform.position.y + ((_playerPencil.position.z - 64) / _mapWidth), 
                transform.position.z);

        for (int y = 0; y < Mathf.Infinity; y++)
        {
            trackingPoint.position = new Vector3(
                transform.position.x + ((_playerPencil.position.x - 64) / _mapWidth), 
                transform.position.y + ((_playerPencil.position.z - 64) / _mapWidth), 
                transform.position.z);
            Color color = Color.black; //((x & y) != 0 ? Color.white : Color.gray);
            texture.SetPixel(Mathf.FloorToInt(_playerPencil.position.x + 192), Mathf.FloorToInt(_playerPencil.position.z + 192), color);
            texture.Apply();
            yield return null;
        }
    }
}
