using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraState : MonoBehaviour
{
    [SerializeField] private Vector3 pillageSceneVec, upgradeSceneVec;

    private void OnEnable()
    {
        EventManager.Instance.OnStateChanged += HandleState;
    }

    private void Start()
    {
        HandleState(GameManager.Instance.currentState);
    }

    void HandleState(GameManager.GameState newState)
    {
        switch (newState)
        {
            case (GameManager.GameState.Upgrade):
                transform.position = upgradeSceneVec;
                break;
            case (GameManager.GameState.Pillage):
                transform.position = pillageSceneVec;
                break;
            case (GameManager.GameState.Pause):
                break;
        }
    }
}
