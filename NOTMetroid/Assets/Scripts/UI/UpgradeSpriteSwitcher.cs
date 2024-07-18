using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSpriteSwitcher : MonoBehaviour
{
    private Image _upgradeImage;
    
    [SerializeField] private Sprite obtainedSprite;

    private void Start()
    {
        _upgradeImage = GetComponent<Image>();
    }

    public void SwitchToObtainedSprite()
    {
        _upgradeImage.sprite = obtainedSprite;
    }
}
