using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{

    public List<Vector3> waypoints = new List<Vector3>();

    public bool looped = false;

    public void OnDrawGizmos()
    {
        int count = looped ? (transform.childCount + 1) : transform.childCount;
        Gizmos.color = Color.cyan;
        for (int i = 1; i < count; i++)
        {
            Transform prev = transform.GetChild(i - 1);
            Transform next = transform.GetChild(i % transform.childCount);
            Gizmos.DrawLine(prev.transform.position, next.transform.position);
            Gizmos.DrawSphere(prev.position, 1);
            Gizmos.DrawSphere(next.position, 1);
        }
    }

	// Use this for initialization
	void Start ()
    {

	}

    public void InitializePath()
    {
        waypoints.Clear();
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            waypoints.Add(transform.GetChild(i).position);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public Vector3 NextWaypoint(int next)
    {
        return waypoints[next];
    }

 
    public bool IsLast(int next)
    {
        return next == waypoints.Count - 1;
    }





}
