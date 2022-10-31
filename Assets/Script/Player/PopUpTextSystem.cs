using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpTextSystem : MonoBehaviour
{
    private GameObject popUpBox;
    private TMP_Text popUpText;
    private GameObject currentNpc;

    [SerializeField] private GameObject interactableSignal;
    [SerializeField] private AudioSource openSound;

    void Update()
    {
        // Teleport
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentNpc != null)
            {
                PopUp();
            }
        }
    }

    public void PopUp()
    {
        openSound.Play();
        popUpBox.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            interactableSignal.SetActive(true);
            currentNpc = collision.gameObject;
            popUpBox = currentNpc.GetComponent<PopUpTextData>().GetPopUpBox();
            popUpText = currentNpc.GetComponent<PopUpTextData>().GetPopUpText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            if (collision.gameObject == currentNpc)
            currentNpc = null;
            interactableSignal.SetActive(false);
        }
    }
}
