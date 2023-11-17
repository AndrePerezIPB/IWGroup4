using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{   
    // UI elements
    public Slider drugsIndicator;
    public GameObject PillsIndicator;
    public TextMeshProUGUI memoryCounterText; 
    public int memoryCount = 0;

    public GameObject player;
    PlayerController playerController;
    [SerializeField] private int redPillCount, bluePillCount;
    [SerializeField] private float maxDrugIndicator;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        // Initialize counts in Start
        redPillCount = playerController.redPillCount;
        bluePillCount = playerController.bluePillCount;

        drugsIndicator.value = 0;
        // drugsIndicator.maxValue = 1; // Set an initial maximum value

        drugsIndicator.interactable = false;
        PillsIndicator = GameObject.Find("PillsIndicator");
        PillsIndicator.SetActive(false);

        memoryCount = playerController.memoryCount;
    }

    void Update()
    {
        drugsIndicatorManager();
        memoryCounterManager();
    }

    private void memoryCounterManager()
    {
        memoryCounterText.SetText(memoryCount.ToString());
    }

    private void drugsIndicatorManager()
    {
        // Update pill counts
        redPillCount = playerController.redPillCount;
        bluePillCount = playerController.bluePillCount;

        // Set the initial maximum value based on the existence of pills
        if (redPillCount > 0 || bluePillCount > 0)
        {
            PillsIndicator.SetActive(true);
            if(redPillCount == 0 && bluePillCount > 0)
            {   
                drugsIndicator.maxValue = 1;
                drugsIndicator.value = 1;
            }
            else if (bluePillCount == redPillCount)
            {
                drugsIndicator.value = 0.5f;
                drugsIndicator.maxValue = 1;
            }
            else if(bluePillCount == 0 && redPillCount > 0)
            {
                drugsIndicator.value = 0;
                drugsIndicator.maxValue = 1;
            }
            else if(bluePillCount > redPillCount)
            {
                drugsIndicator.value = bluePillCount;
                drugsIndicator.maxValue = redPillCount + bluePillCount;
            }
            else if(bluePillCount < redPillCount)
            {   
                drugsIndicator.value = bluePillCount;
                drugsIndicator.maxValue = redPillCount + bluePillCount;
            }
        }
        else
        {
            PillsIndicator.SetActive(false);
            drugsIndicator.maxValue = 1; // Set a default value when no pills are available
        }
    }
}
