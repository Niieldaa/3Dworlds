using UnityEngine;
namespace Scenes
{
    public class WaypointFollow : MonoBehaviour
    {
        [SerializeField] GameObject[] waypoints; //arrays
        int currentWaypointIndex = 0; 
        [SerializeField] float speed = 1f;
        // Update is called once per frame
    

        void Update()
        {
            //Loop for logging waypoints
            for(int i = 0; i < waypoints.Length; i++)    //waypoint.Length tells us how many components is used in unity,
            {
                //log waypoints current name and index in consol
                Debug.Log("Waypoint " + i + " : " + waypoints[i].name);    

            }
            // Changes position twards the waypoint 
            if(Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)  // if the objects position and target position (waypoint) is less than 0.1f, then it ativates.
            {
                currentWaypointIndex++; // adds it up +1

                if (currentWaypointIndex >= waypoints.Length) // if the index is bigger or equal than the highest arraypoint, then changes current to 0 (reset).
                {                                             // this is a nested if statemennt
                    currentWaypointIndex = 0;
                    DestroyObject(gameObject);
                }
            }

            // move with the current waypoint with the assigned speed.
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
        }
    }
}