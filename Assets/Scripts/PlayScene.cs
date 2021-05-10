using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    public Transform fleetSpawn;
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
        

    }






    // Update is called once per frame
    void Update()
    {
        
    }
}
