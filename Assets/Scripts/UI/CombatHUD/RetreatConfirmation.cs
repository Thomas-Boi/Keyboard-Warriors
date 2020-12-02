using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetreatConfirmation : MonoBehaviour
{

    private EventController controller;

    public Button retreat;
    public Button noButton;

    void Start()
    {
        controller = GameObject.Find("EventController").GetComponent<EventController>();
        retreat.onClick.AddListener(() => controller.DisplayPlayerLose());
        noButton.onClick.AddListener(() => gameObject.SetActive(false));
    }    



}
