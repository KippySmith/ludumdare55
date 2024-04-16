using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationMouseOver : MonoBehaviour
{
    public Locations location;
    public CanvasGroup cgGroup;
    public FadeLocationPopup fadeLocation;

    // Start is called before the first frame update
    void Start()
    {
        fadeLocation = cgGroup.GetComponentInParent<FadeLocationPopup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        AudioManager.Instance.PlayMouseOver();
        fadeLocation.FadeIn(location);
    }

    private void OnMouseExit()
    {
        fadeLocation.FadeOut(location);
    }
}
