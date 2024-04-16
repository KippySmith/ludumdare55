using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLocation : MonoBehaviour
{
    private LocationResourcesSO targetLocation;
    [SerializeField] private float taskTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetLocation = gameObject.GetComponentInParent<MinionMovement>().currentTargetSO;

        if (targetLocation != null)
        {
            StartCoroutine(StartTask());
        }
    }

    IEnumerator StartTask()
    {
        while (GameManager.Instance.currentState == GameManager.GameState.Pillage)
        {
            yield return new WaitForSeconds(taskTime);
            TaskComplete();
        }
    }

    void TaskComplete()
    {
        (ResourceTypes resourceType, float weight)[] resourceWeights =
        {
            (ResourceTypes.gold, 1.5f),
            (ResourceTypes.blood, 1.0f),
            (ResourceTypes.bones, 0.7f),
            (ResourceTypes.forbiddenTexts, 0.1f),
            (ResourceTypes.food, 0f),
            (ResourceTypes.demonicArtifacts, 0f)
        };

        float totalWeight = 0f;

        foreach (var (resource, weight) in resourceWeights)
        {
            totalWeight += weight;
        }

        float randomValue = Random.Range(0f, totalWeight);

        float accumulatedWeight = 0f;
        foreach (var (resource, weight) in resourceWeights)
        {
            accumulatedWeight += weight;
            if (randomValue <= accumulatedWeight)
            {
                switch (resource)
                {
                    case ResourceTypes.gold:
                        InventoryManager.Instance.AddResource(resource, Random.Range(1, 10));
                        break;
                    case ResourceTypes.blood:
                        InventoryManager.Instance.AddResource(resource, Random.Range(1, 5));
                        break;
                    case ResourceTypes.bones:
                        InventoryManager.Instance.AddResource(resource, Random.Range(1, 5));
                        break;
                    case ResourceTypes.forbiddenTexts:
                        InventoryManager.Instance.AddResource(resource, 1);
                        break;
                }
                return;
            }
        }
    }
}
