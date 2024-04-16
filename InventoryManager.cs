using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public int count;

    public Dictionary<ResourceTypes, int> resourceAmounts = new Dictionary<ResourceTypes, int>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        resourceAmounts.Add(ResourceTypes.gold, 0);
        resourceAmounts.Add(ResourceTypes.blood, 0);
        resourceAmounts.Add(ResourceTypes.bones, 0);
        resourceAmounts.Add(ResourceTypes.forbiddenTexts, 10);
        resourceAmounts.Add(ResourceTypes.demonicArtifacts, 0);
        resourceAmounts.Add(ResourceTypes.food, 0);
        NewGame();
    }

    void NewGame()
    {
        foreach(var key in resourceAmounts.Keys.ToList())
        {
            resourceAmounts[key] = 0;
        }

        resourceAmounts[ResourceTypes.gold] = 250;
        EventManager.Instance.UpdateResourcesDisplay();

        Debug.Log(resourceAmounts.Values);
    }

    public void AddResource(ResourceTypes resourceType, int amount)
    {
        AudioManager.Instance.InventoryUp();
        resourceAmounts[resourceType] += amount;
        UpdateResourceDisplay();
        Debug.Log("Resource added: " + resourceType + resourceAmounts[resourceType]);
    }

    public void UpdateResourceDisplay()
    {
        EventManager.Instance.UpdateResourcesDisplay();
    }

    public bool HasEnoughResources(params (ResourceTypes, int)[] costs)
    {
        //We take in the costs in a tuple
        foreach (var (resourceType, cost) in costs)
        {
            //Check if our resource amounts list contains the type of resource passed, and the amount of it
            //If either fail, we return false, if not, we proceed to return true
            if (!resourceAmounts.ContainsKey(resourceType) || resourceAmounts[resourceType] < (cost * count) )
            {
                EventManager.Instance.NotEnoughResources();
                return false;
            }
        }
        return true;
    }

    public void ReceiveCount(int newCount)
    {
        count = newCount;
    }

    public bool TryCompletePurchase(params (ResourceTypes, int)[] costs)
    {
        //We run the costs check again
        if (HasEnoughResources(costs))
        {
            // Temporary list to store deductions
            List<(ResourceTypes, int)> resourcesToDeduct = new List<(ResourceTypes, int)>();

            foreach (var (resourceType, cost) in costs)
            {
                Debug.Log("Resource amount: " + resourceType + cost);
                // Add resource and amount for deduction (consider cost * count)
                resourcesToDeduct.Add((resourceType, cost * count));
                Debug.Log("Resource amount: " + resourceType + cost);
            }

            // Deduct resources after loop
            foreach (var (resourceType, cost) in resourcesToDeduct)
            {
                resourceAmounts[resourceType] -= cost;
                Debug.Log("Resource amount: " + resourceType + cost);
                UpdateResourceDisplay();
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}
