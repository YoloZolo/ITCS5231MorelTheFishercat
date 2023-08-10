using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Interactor : MonoBehaviour
{

    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 5f;
    [SerializeField] private LayerMask interactableMask;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    // Update is called once per frame
    void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);

        if (numFound > 0){
            var interactable = colliders[0].GetComponent<IInteractable>();
            if (interactable != null && Input.GetKeyDown("space")){
                interactable.Interact(this);
            }
        }
    }

    /*private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }*/
}
