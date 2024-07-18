using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnergyCounter : MonoBehaviour
{
    [SerializeField] private Image onesDigit;
    [SerializeField] private Image tensDigit;

    [SerializeField] private Sprite[] numberSprites;

    public void RecalculateEnergy(int energyToCheck)
    {
        var currentOnes = 0;
        var currentTens = 0;
        
        for (var i = 0; i < energyToCheck; i++)
        {
            currentOnes++;
            if (currentOnes > 9)
            {
                currentTens++;
                currentOnes = 0;
            }
            onesDigit.sprite = numberSprites[currentOnes];
            tensDigit.sprite = numberSprites[currentTens];
        }

        if (energyToCheck == 0)
        {
            onesDigit.sprite = numberSprites[0];
            tensDigit.sprite = numberSprites[0];
        }
    }
    
}
