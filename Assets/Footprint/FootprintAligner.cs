using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintAligner : MonoBehaviour
{
    public CapsuleCollider playerCol;
    public bool grounded;
    [SerializeField][Header("What layers to hit")]
    private LayerMask _layerMask;
    private ParticleSystem _ps;
    private RingbufferFootSteps _rbfs;
    // Start is called before the first frame update
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        //_ps.Pause();
        
    }
    
    private void FixedUpdate() {
        if(playerCol != null){
            RaycastHit hit;
            if (Physics.Raycast(playerCol.transform.position, Vector3.down, out hit, playerCol.height / 2 + 0.1f, _layerMask)){
                if(!grounded){
                    //_ps.Play();
                }
                grounded = true;
                transform.position = hit.point + Vector3.up * 0.1f;
            }else{
                if(grounded){
                    //_ps.Pause();
                }
                grounded = false;
            }
        }else{
            Debug.Log("Need player collider to know where their feet are!");
        }
        
    }
}
