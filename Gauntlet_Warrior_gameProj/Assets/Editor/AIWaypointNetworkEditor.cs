using UnityEngine;
using System.Collections;
using UnityEditor;

// used to create custom editor// the editor will need 
//to be typecasted to recognize the type of data we need @param AIWaypointNetwork
[CustomEditor (typeof (AIWaypointNetwork))]

public class AIWaypointNetworkEditor : Editor
{
    // override must be provided to override the OnInspectorGui()
    public override void OnInspectorGUI()
    {
        // this will set the ai waypoint targets to a varb
        AIWaypointNetwork network = (AIWaypointNetwork)target;

        network.DisplayMode = (PathDisplayMode)EditorGUILayout.EnumPopup("Display Mode:  ", network.DisplayMode);

        if (network.DisplayMode == PathDisplayMode.PATH)
        {
            network.UIStart = EditorGUILayout.IntSlider("Waypoint Start Path:  ", network.UIStart, 0, network.Waypoints.Count - 1);
            network.UIEnd = EditorGUILayout.IntSlider("Waypoint End Path: ", network.UIEnd, 0, network.Waypoints.Count - 1);
        }
        // this will draw the current default inspector
        DrawDefaultInspector();
    }

    void OnSceneGUI()
    {
        // this will set the ai waypoint targets to a varb
        AIWaypointNetwork network = (AIWaypointNetwork)target;

        // loop through all the pts in the list and output a labe to each of the targets
        for (int i = 0; i + 1 < network.Waypoints.Count; i++)
        {
            if (network.Waypoints[i] != null)
                Handles.Label(network.Waypoints[i].position, "Waypoints " + i.ToString());
        }

        if (network.DisplayMode == PathDisplayMode.CONNECTIONS)
        {
            // container array of line points// hold the network count// will use + 1 to loop from the 0 index to last index
            // make a line that loop around
            Vector3[] linePoints = new Vector3[network.Waypoints.Count + 1];

            for (int i = 0; i <= network.Waypoints.Count; i++)
            {
                // this will assign the index to i
                // if i does NOT EQUAL index of i then will equal 0
                int index = i != network.Waypoints.Count ? i : 0;

                // as long as the waypoint are not null
                if (network.Waypoints[index] != null)
                {
                    // set each position at each index
                    // then store them to line pts array at index i
                    linePoints[i] = network.Waypoints[index].position;
                }
                else
                {
                    // else the point that is null
                    // line size will be set to math inf so it will draw a complete line through the stage
                    // we will see that we have a problem on screen
                    linePoints[i] = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
                }

                // handles the color and drawing the poly line
                Handles.color = Color.green;
                Handles.DrawPolyLine(linePoints);
            }
        }
        else if (network.DisplayMode == PathDisplayMode.PATH)
        {
            NavMeshPath path = new NavMeshPath();

            if (network.Waypoints[network.UIEnd] != null && network.Waypoints[network.UIEnd] != null)
            {
                Vector3 fromStart = network.Waypoints[network.UIStart].position;
                Vector3 toEnd = network.Waypoints[network.UIEnd].position;

                NavMesh.CalculatePath(fromStart, toEnd, NavMesh.AllAreas, path);

                Handles.color = Color.blue;
                Handles.DrawPolyLine(path.corners);
            }
        }
    }

    
}