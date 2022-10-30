using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private float destinationRoomNumber;

    public Transform GetDestination()
    {
        return destination;
    }

    public float GetDestinationRoomNumber()
    {
        return destinationRoomNumber;
    }

}
