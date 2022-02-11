using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

// Copy meshes from children into the parent's Mesh.
// CombineInstance stores the list of meshes.  These are combined
// and assigned to the attached Mesh.

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class MeshCombiner : MonoBehaviour
{
    void Start()
    {
        Invoke("CombineMesh", 0.01f);
    }
    void CombineMesh(){
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];  

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.indexFormat = IndexFormat.UInt32;
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.GetComponent<MeshCollider>().sharedMesh = transform.GetComponent<MeshFilter>().mesh;
        transform.gameObject.SetActive(true);

        transform.position = transform.position / 20;
    }
}