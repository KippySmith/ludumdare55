using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] private float timeBetweenSpawns;

    [SerializeField] private GameObject testMinion;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMinionCoroutine());
    }

    private void OnEnable()
    {
        EventManager.Instance.OnStateChanged += ParseState;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnStateChanged -= ParseState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ParseState(GameManager.GameState newState)
    {
        Debug.Log("Parsing state");
        switch (newState)
        {
            case GameManager.GameState.Upgrade:
                //Do nothing
                break;
            case GameManager.GameState.Pillage:
                //Begin spawning
                GetSpawnInfo();
                break;
            case GameManager.GameState.Pause:
                //Don't spawn anything
                break;
        }
    }

    void GetSpawnInfo()
    {

    }

    void SpawnMinion(GameObject minionType)
    {
        Instantiate(minionType, transform.position, Quaternion.identity);
    }

    IEnumerator SpawnMinionCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnMinion(testMinion);
        }
    }
}
