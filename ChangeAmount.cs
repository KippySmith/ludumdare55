using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeAmount : MonoBehaviour
{
    public TextMeshProUGUI tmpro;

    public DemonCost[] demonType;

    public int count;

    public MinionStatsSO minionType;

    public void Increment()
    {
        Debug.Log("Incremented");
        count = Mathf.Clamp(count += 1, 0, 100);
        UpdateImage();
    }

    public void Decrement()
    {
        Debug.Log("Decremented");
        count = Mathf.Clamp(count -= 1, 0, 100);
        UpdateImage();
    }

    void UpdateImage()
    {
        tmpro.text = count.ToString();
    }

    (ResourceTypes resourceType, int cost)[] GetResourceCosts(DemonType minionType)
    {
        switch (minionType) // Use minionType here
        {
            case DemonType.imp:
                return new (ResourceTypes, int)[] { (ResourceTypes.gold, 50) };
            case DemonType.succubus:
                return new (ResourceTypes, int)[] { (ResourceTypes.gold, 250) };
            case DemonType.hellhound:
                return new (ResourceTypes, int)[] { (ResourceTypes.gold, 500), (ResourceTypes.blood, 500), (ResourceTypes.bones, 500) };
            case DemonType.revenant:
                return new (ResourceTypes, int)[] { (ResourceTypes.gold, 1000), (ResourceTypes.blood, 1000), (ResourceTypes.bones, 1000) };
            case DemonType.Diablo:
                return new (ResourceTypes, int)[] { (ResourceTypes.gold, 10000), (ResourceTypes.blood, 10000), (ResourceTypes.bones, 10000) };
            default:
                return new (ResourceTypes, int)[0]; // Empty array for unexpected cases
        }
    }

    public void SendOrder()
    {
        if (count > 0)
        {
            //Send count info to IM
            InventoryManager.Instance.ReceiveCount(count);
            //Get resources cost from this internal script with a list of costs
            (ResourceTypes resourceType, int cost)[] resourceCosts = GetResourceCosts(minionType.demonType);

            //Check if we have enough resources by passing resourceCosts into IM's HasEnoughResources method
            if (InventoryManager.Instance.HasEnoughResources(resourceCosts))
            {
                //If it returns true, we send the minion type and count thru to MM
                MinionManager.Instance.HandleMinionSummon(minionType, count);
                //We send the resourceCosts through to TryCompletePurchase
                InventoryManager.Instance.TryCompletePurchase(resourceCosts);
                //Reset the count and update the resources on screen
                count = 0;
                UpdateImage();
            }
            else
            {
                Debug.LogWarning("Not enough resources to summon " + minionType.name);
            }
        }
    }
}
