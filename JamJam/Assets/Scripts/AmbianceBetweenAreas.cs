using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceBetweenAreas : MonoBehaviour
{
    public Collider Area;
    [SerializeField] private GameObject Player;
    
    // Update is called once per frame
    void Update()
    {
        // Locate the coloset point on the collider to the player:
        Vector3 closestPoint = Area.ClosestPoint(Player.transform.position);
        // set position to the closest point to the player
        transform.position = closestPoint;
    }
}
