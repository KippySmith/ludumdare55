using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private bool isInInteractRange = false;
    private GameObject interactableGO;
    private GameManager.GameState currentGameState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactableGO = collision.gameObject;
        Debug.Log("Collided with " + collision.name);
        if (collision.TryGetComponent<Interactable>(out Interactable interactable))
        {
            isInInteractRange = true;
            Debug.Log("Interactable is " + interactable);
            EventManager.Instance.EnteredInteractRange();
        }
    }

    private void Update()
    {
        if (currentGameState == GameManager.GameState.Upgrade)
        {
            if (isInInteractRange)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (interactableGO.TryGetComponent<Interactable>(out Interactable interactable))
                    {
                        interactable.Interact();
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactable>(out Interactable interactable))
        {
            isInInteractRange = false;
            EventManager.Instance.ExitedInteractRange();
        }
    }
}
