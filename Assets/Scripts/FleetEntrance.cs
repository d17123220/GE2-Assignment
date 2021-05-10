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
    public GameObject navpointObject;
    public bool canSpawnShips = false;
    private List<GameObject> spawnedShips = null;
    private GameObject spaceVortex = null;
    private GameObject patrolPath = null;
    
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
        patrolPath = CreatePath();

        // enable OnUpdate to create ships
        canSpawnShips = true;
    }

    public GameObject CreatePath()
    {
        // spawn head of the path;
        GameObject pathHead = GameObject.Instantiate<GameObject>(pathObject);
        pathHead.transform.position = this.transform.position;
        pathHead.transform.rotation = this.transform.rotation;
        pathHead.transform.parent = this.transform;

        // spawn navpoints: First one is going to be 4600 units ahead, to match ship's starting point
        GameObject navPoint;
        navPoint = GameObject.Instantiate<GameObject>(navpointObject);
        navPoint.transform.position = this.transform.position + this.transform.forward * 4600.0f;
        navPoint.transform.parent = pathHead.transform;
        
        // spawn navpoints next 5 will be slightly randomized
        for (int i=0; i<5; i++)
        {


            float forward = 20000.0f;
            float up = Random.Range(-1000.0f, 1000.0f);
            float right = Random.Range(-1000.0f, 1000.0f);

            navPoint = GameObject.Instantiate<GameObject>(navpointObject); 
            navPoint.transform.position = this.transform.position 
                                          + this.transform.forward * 4600.0f
                                          + this.transform.forward * forward * (float)(i+1)
                                          + this.transform.up * up
                                          + this.transform.right * right; 
            navPoint.transform.parent = pathHead.transform;
        }

        // initialize path after adding all navpoints
        pathHead.GetComponent<Path>().InitializePath();

        // return path
        return pathHead;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
