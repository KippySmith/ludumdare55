using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadePopup : MonoBehaviour
{
    private CanvasGroup cg;
    // Start is called before the first frame update
    void Start()
    {
        cg = gameObject.GetComponentInParent<CanvasGroup>();
    }

    private void OnEnable()
    {
        EventManager.Instance.OnNotEnoughResources += FadeIn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FadeIn()
    {
        Debug.Log("Fading in");
        cg.DOComplete();
        cg.DOFade(1, 0.1f).OnComplete(FadeOut);
    }

    void FadeOut()
    {
        cg.DOFade(0, 3f).SetEase(Ease.InQuint);
    }
}
