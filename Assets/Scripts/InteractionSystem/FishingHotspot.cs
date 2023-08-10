using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHotspot : MonoBehaviour, IInteractable
{
    public GameObject minigame;
    public GameObject cam1;
    public GameObject cam2;

    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor){
        minigame.SetActive(true);
        cam1.SetActive(false);
        cam2.SetActive(true);
        Debug.Log("Catching Fish!");
        return true;
    }
}
