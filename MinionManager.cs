using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    public static MinionManager Instance { get; private set; }

    public float timeBetweenSpawns;

    public List<GameObject> minionsImps = new List<GameObject>();
    public List<GameObject> minionsSuccubi = new List<GameObject>();
    public List<GameObject> minionsHounds = new List<GameObject>();
    public List<GameObject> minionsRevenants = new List<GameObject>();
    public List<GameObject> minionsDiablo = new List<GameObject>();

    List<GameObject> chosenList;

    public GameObject diablo;

    public MinionStatsSO impSO, succubusSO, hellhoundSO, revenantSO, diabloSO;

    public GameObject spawnObject;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        timeBetweenSpawns = 0.25f;
    }

    private void OnEnable()
    {
        EventManager.Instance.OnStateChanged += ParseState;
    }

    public void HandleMinionSummon(MinionStatsSO minionSO, int amount)
    {
        //This function adds minions to their respective lists to them be spawned from SpawnDemon
        GameObject newDemon = minionSO.demonPrefab;
        switch (minionSO.demonType)
        {
            case (DemonType.imp):
                for (int i = 0; i < amount; i++)
                {
                    minionsImps.Add(newDemon);
                }
                break;
            case (DemonType.succubus):
                for (int i = 0; i < amount; i++)
                {
                    minionsSuccubi.Add(newDemon);
                }
                break;
            case (DemonType.hellhound):
                for (int i = 0; i < amount; i++)
                {
                    minionsHounds.Add(newDemon);
                }
                break;
            case (DemonType.revenant):
                for (int i = 0; i < amount; i++)
                {
                    minionsRevenants.Add(newDemon);
                }
                break;
            case (DemonType.Diablo):
                for (int i = 0; i < amount; i++)
                {
                    minionsDiablo.Add(newDemon);
                }
                break;
                // ... similar cases for other minion types
        }
    }

    public void BeginSummoning()
    {
        Debug.Log("Begin summoning");
        StartCoroutine(SpawnDemon());
    }

    void ParseState(GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.Pillage)
        {
            BeginSummoning();
        }
    }

    public DemonType GetRandomDemonType()
    {
        DemonType demonType = (DemonType)Random.Range(0, 4);
        return demonType;
    }

    IEnumerator SpawnDemon()
    {
        Debug.Log("In spawning 1");
        yield return new WaitForSeconds(0.75f);
        while (GameManager.Instance.currentState == GameManager.GameState.Pillage)
        {
            Debug.Log("In spawning 2");
            yield return new WaitForSeconds(timeBetweenSpawns);
            DemonType randomType = GetRandomDemonType();

            switch (randomType)
            {
                case DemonType.imp:
                    chosenList = minionsImps;
                    break;
                case DemonType.succubus:
                    chosenList = minionsSuccubi;
                    break;
                case DemonType.hellhound:
                    chosenList = minionsHounds;
                    break;
                case DemonType.revenant:
                    chosenList = minionsRevenants;
                    break;
                case DemonType.Diablo:
                    //Handle diablo spawn
                    chosenList = minionsDiablo;
                    Debug.Log("Trying to spawn diablo");
                    break;
                default:
                    chosenList = null;
                    break;
            }

            if (chosenList != null && chosenList.Count > 0)
            {
                int randomIndex = Random.Range(0, chosenList.Count);
                GameObject minionToSpawn = chosenList[randomIndex];
                Instantiate(minionToSpawn, spawnObject.transform.position, Quaternion.identity);

                // Remove the spawned Imp from the list
                chosenList.RemoveAt(randomIndex);
            }
        }
    }
}
