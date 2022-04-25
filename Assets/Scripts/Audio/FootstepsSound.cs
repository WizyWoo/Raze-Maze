using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter _audio;
    private void Start()
    {
        _audio = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            _audio.Play();
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            _audio.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jungle"))
        {
            _audio.Play();
            _audio.SetParameter("Terrain", 1);
        }
    }
}
