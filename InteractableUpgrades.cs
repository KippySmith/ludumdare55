using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableUpgrades : Interactable
{
    public override void Interact()
    {
        EventManager.Instance.OpenedUpgradeMenu();
    }
}
