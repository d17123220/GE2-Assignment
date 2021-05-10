using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool canFollow = false;
    public GameObject target = null;
    public float timePassed = 0.0f;
    public float timeToFollow = 0.0f;


    public void StartFollowing()
    {
        canFollow = true;
    }

    public void SeekTarget()
    {
        // find a new ship
        List<GameObject> ships = new List<GameObject>();
        
        foreach (var obj in GameObject.FindGameObjectsWithTag ("Narn_Big_Ship"))
        {
            ships.Add( (GameObject) obj);
        }

        if (ships.Count == 0)
            // if no ships found - do nothing
            return; 

        int shipNum = Random.Range(0, ships.Count);
        target = ships[shipNum];

        // reset passed timer
        timePassed = 0.0f;
        // get new random time to follow this target
        timeToFollow = Random.Range(5.0f, 25.0f);
        // jump camera closer to this ship

        Vector3 newPosition;
        float distance = 0.0f;
        
        // new random position within sphere with radius of 12500, but also no less than 7500 from the center
        do
        {
            newPosition = Random.insideUnitSphere * 7500.0f;
            distance = Vector3.Distance(newPosition, Vector3.zero);
        }
        while (distance < 2000.0f);

        // jump to this position around target ship
        transform.position = target.transform.position + newPosition;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canFollow)
        {
            if (null != target)
            {
                if (timePassed < timeToFollow)
                {
                    // look at the target
                    transform.LookAt(target.transform.position);
                    // increment timer
                    timePassed += Time.deltaTime;
                }
                else
                {
                    // time to look at this ship exceeded, let's look at another one
                    SeekTarget();
                }
            }
            else
            {
                // no ship to look at, let's seek a new one
                SeekTarget();
            }
        }
    }
}
