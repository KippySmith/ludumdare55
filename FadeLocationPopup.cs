using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeLocationPopup : MonoBehaviour
{
    [SerializeField] private CanvasGroup forestCG, archivesCG, villageCG, tombsCG, treasuryCG;
    private float fullFade = 1;
    private float noFade = 0;
    private float duration = 0.1f;
    private float fadeoutDuration = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn(Locations location)
    {
        Debug.Log("Fadein called");
        switch (location)
        {
            case Locations.forest:
                forestCG.DOComplete();
                forestCG.DOFade(fullFade, duration);
                break;
            case Locations.village:
                villageCG.DOComplete();
                villageCG.DOFade(fullFade, duration);
                break;
            case Locations.archives:
                archivesCG.DOComplete();
                archivesCG.DOFade(fullFade, duration);
                break;
            case Locations.tombs:
                tombsCG.DOComplete();
                tombsCG.DOFade(fullFade, duration);
                break;
            case Locations.treasury:
                treasuryCG.DOComplete();
                treasuryCG.DOFade(fullFade, duration);
                break;
        }
    }

    public void FadeOut(Locations location)
    {
        switch (location)
        {
            case Locations.forest:
                forestCG.DOComplete();
                forestCG.DOFade(noFade, fadeoutDuration);
                break;
            case Locations.village:
                villageCG.DOComplete();
                villageCG.DOFade(noFade, fadeoutDuration);
                break;
            case Locations.archives:
                archivesCG.DOComplete();
                archivesCG.DOFade(noFade, fadeoutDuration);
                break;
            case Locations.tombs:
                tombsCG.DOComplete();
                tombsCG.DOFade(noFade, fadeoutDuration);
                break;
            case Locations.treasury:
                treasuryCG.DOComplete();
                treasuryCG.DOFade(noFade, fadeoutDuration);
                break;
        }
    }
}
