using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PathDisplayMode
{
    NONE,
    CONNECTIONS,
    PATH,

}
public class AIWaypointNetwork: MonoBehaviour
{
    [HideInInspector]
    public int UIStart = 0;
    [HideInInspector]
    public int UIEnd = 0;
    [HideInInspector]
    public PathDisplayMode DisplayMode = PathDisplayMode.CONNECTIONS;

    public List<Transform> Waypoints = new List<Transform>();

}
