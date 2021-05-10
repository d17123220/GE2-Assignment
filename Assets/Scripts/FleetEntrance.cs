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
        yield return new  WaitForSeconds(7.0f);

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
        navPoint.transform.rotation = this.transform.rotation;
        navPoint.transform.parent = pathHead.transform;
        
        GameObject lastPont;
        float maxAngle;

        // spawn navpoints next 5 will be slightly randomized
        for (int i=0; i<5; i++)
        {
            lastPont = navPoint;

            // forward units to travel based on direction of last navpoint
            float forward = 20000.0f;
            
            if (i == 0)
                maxAngle = 15.0f;  //Mathf.PI / 6.0f; // 30 degrees
            else
                maxAngle = 80.0f;  //4.0f * Mathf.PI / 9.0f; // 80 degrees
            

            lastPont.transform.Rotate(new Vector3(Random.Range(-1*maxAngle, maxAngle), Random.Range(-1*maxAngle,maxAngle), 0));

            navPoint = GameObject.Instantiate<GameObject>(navpointObject); 
            navPoint.transform.position = lastPont.transform.position + lastPont.transform.forward * forward;
            navPoint.transform.rotation = lastPont.transform.rotation;
            navPoint.transform.parent = pathHead.transform;
        }

        // initialize path after adding all navpoints
        pathHead.GetComponent<Path>().InitializePath();

        // return path
        return pathHead;
    }

    public void DestroyVortex()
    {
        if (null != spaceVortex)
        {
            Destroy(spaceVortex);
            spaceVortex = null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        // if can spawn ships now - meaning vortex is open and patrol path created
        if (canSpawnShips)
        {

            
            // if already 3 ships spawned
            if (spawnedShips.Count == 3)
            {
                canSpawnShips = false;
                Invoke("DestroyVortex", 3.0f);
            }
            else
            {
                // get distance to the closest ship
                float minDistance = -1.0f;
                float curDistance = 0.0f;

                // parse each ship
                foreach (GameObject ship in spawnedShips)
                {
                    // get distance to the ship
                    curDistance = Vector3.Distance(this.transform.position, ship.transform.position);
                    // get the minimal distance, which is less than minDistance and not -1
                    if (minDistance == -1.0f || curDistance < minDistance)
                        minDistance = curDistance;  
                }

                // 12000 is a minimum distance ship need to travel before next one is spawned
                if (minDistance >= 12000.0f || minDistance == -1.0f)
                {
                    GameObject ship =  GameObject.Instantiate<GameObject>(bigShipObject);
                    ship.transform.rotation = this.transform.rotation;
                    ship.transform.position = this.transform.position + this.transform.forward * 4500.0f;
                    // adjust offset off path only for second and third ship
                    if (spawnedShips.Count > 0)
                    {
                        ship.GetComponent<PathFollowBehaviour>().radiusOffset = 2500.0f;
                        ship.GetComponent<PathFollowBehaviour>().angleOffset = Random.Range(0.0f, 2 * Mathf.PI); 
                    }
                    ship.GetComponent<PathFollowBehaviour>().path = patrolPath.GetComponent<Path>();
                    ship.GetComponent<PathFollowBehaviour>().StartFollowing();

                    spawnedShips.Add(ship);
                }
            }
        }
    }
}
