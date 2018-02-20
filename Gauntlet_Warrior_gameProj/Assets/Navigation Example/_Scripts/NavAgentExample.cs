using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentExample : MonoBehaviour {

    public AIWaypointNetwork WaypointNetwork = null;
    public int currentIndexWaypoint = 0;
    public bool doeshavePath = false;
    public bool isPathPending = false;
    public bool isPathStale = false;
    public NavMeshPathStatus pathStatus = NavMeshPathStatus.PathInvalid;

    // inspector assigned varb of type NavMesh @param m_navAgent
    private NavMeshAgent m_navAgent = null;

    // Use this for initialization
    void Start() {
        // how to cache a nav mesh agent reference
        m_navAgent = GetComponent<NavMeshAgent>();

        if (WaypointNetwork == null)
        {
            Debug.Log("NavAgentExample::Start()->waypoint network null!");
            return;
        }

        /*
        //test code
        // as long as we have no null waypoints in out current waypoint index
        if (WaypointNetwork.Waypoints[currentWaypoint] != null)
        {
            // have our nav mesh agent travel to a destination// the magic is here
            m_navAgent.destination = WaypointNetwork.Waypoints[currentWaypoint].position;
        }
        */

        SetNextDestination(false);
    }

    void SetNextDestination(bool increment)
    {
        if (!WaypointNetwork)
        {
            print("NavAgentExample::SetDestination(bool):: we have no waypoint network, is null.");
            return;
        }
        //used with bool// if true = 1 else = 0
        int incrementStep = increment ? 1 : 0;
        Transform nextWaypointTransform = null;

        // while we are serching for the next waypoint position
        // if next way point not null// go to the next pos
        int nextWaypoint = (currentIndexWaypoint + incrementStep >= WaypointNetwork.Waypoints.Count) ? 0 : currentIndexWaypoint + incrementStep;
        nextWaypointTransform = WaypointNetwork.Waypoints[nextWaypoint];

        if (nextWaypointTransform != null)
        {
            currentIndexWaypoint = nextWaypoint;
            m_navAgent.destination = nextWaypointTransform.position;
            return;
        }

        currentIndexWaypoint++;
    }

    // Update is called once per frame
    void Update() {

        doeshavePath = m_navAgent.hasPath;
        isPathPending = m_navAgent.pathPending;
        pathStatus = m_navAgent.pathStatus;

        if ((!doeshavePath && !isPathPending) || pathStatus == NavMeshPathStatus.PathInvalid)
        {
            SetNextDestination(true);
        }
        else if (m_navAgent.isPathStale)
        {
            SetNextDestination(false);
        }
	
	}
}
