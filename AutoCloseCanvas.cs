using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCloseCanvas : MonoBehaviour
{
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("CloseCanvas", duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CloseCanvas()
    {
        gameObject.SetActive(false);
    }
}
