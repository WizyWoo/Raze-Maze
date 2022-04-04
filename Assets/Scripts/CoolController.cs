using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CoolController : MonoBehaviour
{

    public float PlayerSize;
    public VideoClip[] Clips;
    private VideoPlayer vidPlayer;

    private void Start()
    {

        vidPlayer = gameObject.GetComponent<VideoPlayer>();

        vidPlayer.clip = Clips[Random.Range(0, Clips.Length)];
        vidPlayer.Play();

        gameObject.transform.localScale = new Vector3(vidPlayer.clip.width * PlayerSize, vidPlayer.clip.height * PlayerSize, 1);

    }

}
