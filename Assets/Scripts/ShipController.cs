using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public bool escapeVortex = false;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new PatrolState());        
    }

    public void OpenVortex(Vector3 target)
    {
        // open space vortex        
        
        
        escapeVortex = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
