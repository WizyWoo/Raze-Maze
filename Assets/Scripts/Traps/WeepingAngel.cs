using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeepingAngel : TrapController
{
    public GameObject player;
    private new Camera camera;
    private new Renderer renderer;
    private float closeCounter = 0f;
    private Vector3 cameraLookForward;
    private float rayLength = 1f;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        camera = Camera.main;
        renderer = GetComponentInChildren<Renderer>();
    }

    private void Update()
    {
        bool isVisible = GeometryUtility.TestPlanesAABB(
            GeometryUtility.CalculateFrustumPlanes(camera),
            renderer.bounds);

        if (!isVisible)
            TryMovingTowardsPlayer();
    }

    private void TryMovingTowardsPlayer()
    {
        if (player == null)
            return;

        cameraLookForward = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z).normalized;
        
        closeCounter += Time.deltaTime;

        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, -cameraLookForward, out hit, rayLength, ~(1 << LayerMask.NameToLayer("Player")), QueryTriggerInteraction.Ignore))
             transform.position = hit.transform.position;
        else
             transform.position = Vector3.Lerp(transform.position, camera.transform.position - cameraLookForward, closeCounter);
        
        Vector3 lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
