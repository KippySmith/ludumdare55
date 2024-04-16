using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource mouseOverAS, upgradeAS, menuOpenAS, menuCloseAS, notEnoughAS, pageTurnAS, inventoryAS;

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
    }

    void OnEnable()
    {

    }

    public void PlayMouseOver()
    {
        mouseOverAS.Play();
    }

    public void PlayUpgrade()
    {
        upgradeAS.Play();
    }

    public void OpenMenu()
    {
        menuOpenAS.Play();
    }

    public void CloseMenu()
    {
        menuCloseAS.Play();
    }

    public void PlayNotEnoughResources()
    {
        notEnoughAS.Play();
    }

    public void PlayPageTurn()
    {
        pageTurnAS.Play();
    }

    public void InventoryUp()
    {
        inventoryAS.Play();
    }
}
