using UnityEngine;
using UnityEngine.AI;

public class RingbufferFootSteps : MonoBehaviour
{
    public ParticleSystem system;
    public Material activeMat;

    Vector3 lastEmit;

    public float delta = 1;
    public float gap = 0.5f;
    int dir = 1;
    static RingbufferFootSteps selectedSystem;
    private FootprintAligner _fa;

    void Start()
    {
        lastEmit = transform.position;
        _fa = GetComponent<FootprintAligner>();
    }

    public void Update()
    {
        if(_fa.grounded || _fa == null){
            if (Vector3.Distance(lastEmit, transform.position) > delta){
                Gizmos.color = Color.green;
                var pos = transform.position + (transform.right * gap * dir);
                dir *= -1;
                ParticleSystem.EmitParams ep = new ParticleSystem.EmitParams();
                ep.position = pos;
                ep.rotation = transform.rotation.eulerAngles.y;
                system.Emit(ep, 1);
                lastEmit = transform.position;
            }
        }

    }
}
