using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TeleportController : MonoBehaviour
{
    //Teleporter
    private GameObject currentTeleporter;
    // InteractableObejct
    public GameObject interactable;

    float destinationRoomNumber;

    [SerializeField] private CinemachineVirtualCamera vcamRoom1;
    [SerializeField] private CinemachineVirtualCamera vcamRoom2;
    [SerializeField] private CinemachineVirtualCamera vcamRoom3;
    [SerializeField] private CinemachineVirtualCamera vcamRoom4;
    [SerializeField] private CinemachineVirtualCamera vcamRoom5;


    void Start()
    {
        var vcamRoom1 = GetComponent<CinemachineVirtualCamera>();
        var vcamRoom2 = GetComponent<CinemachineVirtualCamera>();
        var vcamRoom3 = GetComponent<CinemachineVirtualCamera>();
        var vcamRoom4 = GetComponent<CinemachineVirtualCamera>();
        var vcamRoom5 = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        // Teleport
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTeleporter != null)
            {
                transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
                ChangeCamToRoom();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Teleport
        if (collision.CompareTag("Teleporter"))
        {
            interactable.SetActive(true);
            currentTeleporter = collision.gameObject;
            destinationRoomNumber = currentTeleporter.GetComponent<Teleporter>().GetDestinationRoomNumber();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Teleport
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            currentTeleporter = null;
            destinationRoomNumber = 0;
            interactable.SetActive(false);
        }
    }

    public void ChangeCamToRoom()
    {
        if (destinationRoomNumber == 1)
        {
            priorityRoom1();
        }
        if (destinationRoomNumber == 2)
        {
            priorityRoom2();
        }
        if (destinationRoomNumber == 3)
        {
            priorityRoom3();
        }
        if (destinationRoomNumber == 4)
        {
            priorityRoom4();
        }
        if (destinationRoomNumber == 5)
        {
            priorityRoom5();
        }
    }

    public void priorityRoom1()
    {
        vcamRoom1.Priority = 1;
        vcamRoom2.Priority = 0;
        vcamRoom3.Priority = 0;
        vcamRoom4.Priority = 0;
    }

    public void priorityRoom2()
    {
        vcamRoom1.Priority = 0;
        vcamRoom2.Priority = 1;
        vcamRoom3.Priority = 0;
        vcamRoom4.Priority = 0;
    }

    public void priorityRoom3()
    {
        vcamRoom1.Priority = 0;
        vcamRoom2.Priority = 0;
        vcamRoom3.Priority = 1;
        vcamRoom4.Priority = 0;
    }

    public void priorityRoom4()
    {
        vcamRoom1.Priority = 0;
        vcamRoom2.Priority = 0;
        vcamRoom3.Priority = 0;
        vcamRoom4.Priority = 1;
    }

    public void priorityRoom5()
    {
        vcamRoom1.Priority = 0;
        vcamRoom2.Priority = 0;
        vcamRoom3.Priority = 0;
        vcamRoom4.Priority = 0;
        vcamRoom5.Priority = 1;
    }
}
