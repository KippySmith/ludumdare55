using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    public enum MinionMovementState { toDestination, atDestination, toHome };
    public MinionMovementState currentState;
    public Vector2 targetPos;
    public MinionStatsSO minionStats;
    public float minionSpeed;

    public List<GameObject> locationObjects = new List<GameObject>();
    public LocationResourcesSO forest, treasury, library, tombs, village;
    public List<LocationResourcesSO> locationSOs = new List<LocationResourcesSO>();

    public LocationResourcesSO currentTargetSO;

    private void Awake()
    {
        /*        locationObjects.Add(forest);
                locationObjects.Add(treasury);
                locationObjects.Add(library);
                locationObjects.Add(tombs);
                locationObjects.Add(village);*/
        locationSOs.Add(forest);
        locationSOs.Add(treasury);
        locationSOs.Add(library);
        locationSOs.Add(tombs);
        locationSOs.Add(village);

        ChoseRandomLocation();
    }

    private void OnEnable()
    {
        EventManager.Instance.OnPillageEnding += ReturnHome;
    }
    // Start is called before the first frame update
    void Start()
    {
        minionSpeed = minionStats.speed;

        currentState = MinionMovementState.toDestination;
    }

    void ChoseRandomLocation()
    {
        int newRandom = Random.Range(0, 4);
        LocationResourcesSO randomLocation = locationSOs[newRandom];
        currentTargetSO = randomLocation;

        ReceiveLocationTarget(randomLocation.locationPosition);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case MinionMovementState.toDestination:
                HeadToDestination();
                break;
            case MinionMovementState.atDestination:
                break;
            case MinionMovementState.toHome:
                break;
        }

    }

    void ReceiveLocationTarget(Vector2 target)
    {
        targetPos = target;
    }

    public void HeadToDestination()
    {
        Vector2 newTrans = new Vector2(transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(newTrans, targetPos, minionSpeed * Time.deltaTime);
    }

    public void ReturnHome()
    {

    }
}
