using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetEntrance : MonoBehaviour
{
    private float spawnDelay = 0.0f;
    public GameObject pathObject;
    public GameObject bigShipObject;
    public GameObject smallShipObject;
    public GameObject vortexObject;
    public bool canSpawnShips = false;
    private List<GameObject> spawnedShips = null;
    private GameObject spaceVortex = null;
    
    // Start is called before the first frame update
    void Start()
    {
        // initialize a new list of big ships
        spawnedShips = new List<GameObject>();
    }

    public void StartSpawn(float delay)
    {
        spawnDelay = delay;
        StartCoroutine(EnterTheFleet());
    }
    
    System.Collections.IEnumerator EnterTheFleet()
    {
        // wait for first delay
        yield return new WaitForSeconds(spawnDelay);


        // create a partical vortex
        spaceVortex = GameObject.Instantiate<GameObject>(vortexObject);
        spaceVortex.transform.parent = this.transform;
        spaceVortex.transform.rotation = this.transform.rotation;
        spaceVortex.transform.position = this.transform.position;


        // wait for small delay for it to finalize
        yield return new  WaitForSeconds(2.0f);

        // create a path


        // enable OnUpdate to create ships
        canSpawnShips = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
