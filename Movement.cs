using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private GameManager.GameState currentState;
    [SerializeField] private Animator animator;

    [SerializeField] private float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventManager.Instance.OnStateChanged += HandleState;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameManager.GameState.Upgrade)
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

            transform.position += moveDirection * speed * Time.deltaTime;

            if (moveDirection.x != 0 || moveDirection.y != 0)
            {
                animator.Play("Walk");
            }
        }
    }

    void HandleState(GameManager.GameState newState)
    {
        currentState = newState;
    }
}
