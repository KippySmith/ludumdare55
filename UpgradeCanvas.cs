using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UpgradeCanvas : MonoBehaviour
{
    public static UpgradeCanvas Instance;

    [SerializeField] private GameObject demonSummoningCanvas, portalUpgradeCanvas, youWin, youLose;

    [SerializeField] private TextMeshProUGUI goldDisplay, bloodDisplay, bonesDisplay, textsDisplay, artifactsDisplay, foodDisplay;

    [SerializeField] private CanvasGroup interactCG;

    [SerializeField] private int pagesUnlocked;

    [SerializeField] public enum Pages { imp, succubus, hellhound, revenant, diablo };

    [SerializeField] GameObject impPage, succubusPage, hellhoundPage, revenantPage, diabloPage;
    [SerializeField] GameObject impNextArrow, succubusNextArrow, hellhoundNextArrow, revenantNextArrow;

    public Pages currentPage;

    [SerializeField] private TextMeshProUGUI summoningUpgradeText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DisplayResourceAmounts();
        pagesUnlocked = 0;
    }

    private void OnEnable()
    {
        EventManager.Instance.OnResourceDisplayUpdated += DisplayResourceAmounts;

        EventManager.Instance.OnEnteredInteractRange += FadeInInteractCG;
        EventManager.Instance.OnExitedInteractRange += FadeOutInteractCG;

        EventManager.Instance.OnOpenedNecronomiconMenu += OpenNecronomicon;
        EventManager.Instance.OnOpenedUpgradeMenu += OpenUpgrade;

        EventManager.Instance.OnPageChangedNecro += ChangePage;
        EventManager.Instance.OnNewPageUnlocked += UnlockNextPage;

        EventManager.Instance.OnGameWin += YouWin;
        EventManager.Instance.OnGameOver += YouLose;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void YouWin()
    {
        youWin.SetActive(true);
    }

    void YouLose()
    {
        youLose.SetActive(true);
    }

    void DisplayResourceAmounts()
    {
        foreach (var resource in InventoryManager.Instance.resourceAmounts)
        {
            switch (resource.Key)
            {
                case ResourceTypes.gold:
                    goldDisplay.text = resource.Value.ToString();
                    break;
                case ResourceTypes.blood:
                    bloodDisplay.text = resource.Value.ToString();
                    break;
                case ResourceTypes.bones:
                    bonesDisplay.text = resource.Value.ToString();
                    break;
                case ResourceTypes.forbiddenTexts:
                    textsDisplay.text = resource.Value.ToString();
                    break;
                case ResourceTypes.demonicArtifacts:
                    artifactsDisplay.text = resource.Value.ToString();
                    break;
                case ResourceTypes.food:
                    foodDisplay.text = resource.Value.ToString();
                    break;
            }
        }
    }

    void OpenNecronomicon()
    {
        AudioManager.Instance.OpenMenu();
        Debug.Log("Opening necro");
        demonSummoningCanvas.SetActive(true);
        ChangePage(0);
    }

    void OpenUpgrade()
    {
        AudioManager.Instance.OpenMenu();
        Debug.Log("Opening upgrades");
        portalUpgradeCanvas.SetActive(true);
    }

    void FadeInInteractCG()
    {
        interactCG.DOComplete();
        interactCG.DOFade(1, 0.25f);
    }

    void FadeOutInteractCG()
    {
        interactCG.DOComplete();
        interactCG.DOFade(0, 0.1f);
    }

    void ChangePage(int pageNumber)
    {
        AudioManager.Instance.PlayPageTurn();
        switch (pageNumber)
        {
            case (0):
                currentPage = Pages.imp;

                impPage.SetActive(true);
                succubusPage.SetActive(false);
                hellhoundPage.SetActive(false);
                revenantPage.SetActive(false);
                diabloPage.SetActive(false);
                break;
            case (1):
                currentPage = Pages.succubus;

                succubusPage.SetActive(true);
                impPage.SetActive(false);
                hellhoundPage.SetActive(false);
                revenantPage.SetActive(false);
                diabloPage.SetActive(false);
                break;
            case (2):
                currentPage = Pages.hellhound;

                hellhoundPage.SetActive(true);
                impPage.SetActive(false);
                succubusPage.SetActive(false);
                revenantPage.SetActive(false);
                diabloPage.SetActive(false);
                break;
            case (3):
                currentPage = Pages.revenant;

                revenantPage.SetActive(true);
                impPage.SetActive(false);
                hellhoundPage.SetActive(false);
                succubusPage.SetActive(false);
                diabloPage.SetActive(false);
                break;
            case (4):
                currentPage = Pages.diablo;

                diabloPage.SetActive(true);
                impPage.SetActive(false);
                hellhoundPage.SetActive(false);
                revenantPage.SetActive(false);
                succubusPage.SetActive(false);
                break;
        }
    }

    void UnlockNextPage()
    {
        if (pagesUnlocked < 4)
        {
            switch (pagesUnlocked)
            {
                case (0):
                    summoningUpgradeText.text = "Summoning Page 2/5";
                    pagesUnlocked += 1;
                    impNextArrow.SetActive(true);
                    break;
                case (1):
                    summoningUpgradeText.text = "Summoning Page 3/5";
                    pagesUnlocked += 1;
                    succubusNextArrow.SetActive(true);
                    break;
                case (2):
                    summoningUpgradeText.text = "Summoning Page 4/5";
                    pagesUnlocked += 1;
                    hellhoundNextArrow.SetActive(true);
                    break;
                case (3):
                    summoningUpgradeText.text = "Summoning Page 5/5";
                    pagesUnlocked += 1;
                    revenantNextArrow.SetActive(true);
                    break;
            }
        }

        else
        {
            //Throw all pages unlocked error
        }
    }

    public void OnUnlockNecroPageClicked()
    {
        (ResourceTypes resourceType, int cost) purchaseInfo = (ResourceTypes.forbiddenTexts, 5);

        if (InventoryManager.Instance.HasEnoughResources(purchaseInfo))
        {
            if (InventoryManager.Instance.TryCompletePurchase(purchaseInfo))
            {
                UnlockNextPage();
                AudioManager.Instance.PlayUpgrade();
                Debug.Log("We unlocked it");
            }
            else
            {
                Debug.Log("We failed to unlock it");
            }
        }
        else
        {
            AudioManager.Instance.PlayNotEnoughResources();
            Debug.Log("Not enough resources");
        }
    }

    void UpdateUpgradeText()
    {

    }
}

