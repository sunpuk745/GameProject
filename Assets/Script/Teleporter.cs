using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private Transform destinationRoomCamera;
    [SerializeField] private Transform room1Cam;
    [SerializeField] private Transform room2Cam;

    //[SerializeField] private NewPlayerMovement player;
    //[SerializeField] private Transform cameraRoom1;
    //[SerializeField] private Transform cameraRoom2;
    //[SerializeField] private Transform cameraRoom3;
    //[SerializeField] private Transform cameraRoom4;

    void Start()
    {
        var vcam = GetComponent<CinemachineVirtualCamera>();
    }

    public Transform GetDestination()
    {
        return destination;
    }

    public void changeCameraToRoom1()
    {
        vcam.LookAt = room1Cam;
        vcam.Follow = room1Cam;
    }

    public void changeCameraToRoom2()
    {
        vcam.LookAt = room2Cam;
        vcam.Follow = room2Cam;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                vcam.Follow = destinationRoomCamera;
                vcam.LookAt = destinationRoomCamera;
            }
        }
    }



    //public void Teleport()
    //{
    //    player.transform.position = player.currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
    //}
}
