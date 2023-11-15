using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{   
    //ui elements
    public Slider drugsIndicator;
    public GameObject PillsIndicator;

    public GameObject player;
    PlayerController playerController;
    [SerializeField]
    private int redPillCount, bluePillCount;
    [SerializeField]
    private float maxDrugIndicator;
    [SerializeField]
    private float scale;
    void Start()
    {   
        playerController = player.GetComponent<PlayerController>();
        redPillCount = playerController.redPillCount;
        bluePillCount = playerController.bluePillCount;
        drugsIndicator.value = scale;
        drugsIndicator.maxValue = maxDrugIndicator;

        drugsIndicator.interactable = false;
        PillsIndicator = GameObject.Find("PillsIndicator");
        PillsIndicator.SetActive(false);
    }

    void Update()
    {
        drugsIndicatorManager();
    }

    private void drugsIndicatorManager()
    {
        redPillCount = playerController.redPillCount;
        bluePillCount = playerController.bluePillCount;
        maxDrugIndicator = redPillCount + bluePillCount;
        scale = bluePillCount;
        if(redPillCount > 0 || bluePillCount > 0)
        {
            PillsIndicator.SetActive(true);
        }
        
        if(redPillCount < 1)
        {
            maxDrugIndicator = 1;
            scale = 1;
        }

        maxDrugIndicator = redPillCount + bluePillCount;
        scale = bluePillCount / maxDrugIndicator;
        drugsIndicator.maxValue = maxDrugIndicator;
        drugsIndicator.value = scale;
    }
}
