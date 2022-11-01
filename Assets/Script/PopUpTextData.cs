using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpTextData : MonoBehaviour
{
    [SerializeField] private GameObject popUpBox;
    [SerializeField] private Animator popUpBoxAnimator;
    [SerializeField] private TMP_Text popUpText;
    [SerializeField] private float closingTime;
    [SerializeField] private AudioSource closeSound;
    private bool IsOpen = false;

    void Update()
    {
        // Teleport
        if (Input.GetKeyDown(KeyCode.E) && IsOpen == true)
        {
            ClosePopUpBox();
        }
        if (popUpBox.activeSelf == true)
        {
            IsOpen = true;
        }
        else{
            IsOpen = false;
        }
    }
    
    public GameObject GetPopUpBox()
    {
        return popUpBox;
    }

    public Animator GetPopAnim()
    {
        return popUpBoxAnimator;
    }

    public TMP_Text GetPopUpText()
    {
        return popUpText;
    }

    public IEnumerator Close()
    {
        popUpBoxAnimator.SetTrigger("pop");
        closeSound.Play();
        yield return new WaitForSeconds(closingTime);
        popUpBox.SetActive(false);
    }

    public void ClosePopUpBox()
    {
        StartCoroutine(Close());
    }
}
