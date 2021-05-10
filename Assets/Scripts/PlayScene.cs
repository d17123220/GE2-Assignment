using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    public GameObject fleetSpawn;
    private GameObject mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (mainCamera == null)
        {
            mainCamera = Camera.main.gameObject;
        }
        Invoke("SpawnFleet",1);


    }

    public void SpawnFleet()
    {
        float delay = 0.0f;
        foreach (Transform child in fleetSpawn.transform)
        {
            // tell vortex to start spawning, and then next one start with increased delay
            child.GetComponent<FleetEntrance>().StartSpawn(delay);
            delay += 4.0f;
        }
        
        mainCamera.GetComponent<CameraFollow>().StartFollowing();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
