using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSummoning : Interactable
{
    public override void Interact()
    {
        EventManager.Instance.OpenedNecronomiconMenu();
    }
}
