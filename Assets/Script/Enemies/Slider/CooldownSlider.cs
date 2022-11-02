using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownSlider : MonoBehaviour
{
    [SerializeField]private Enemy3 enemy;
    public Slider slider;
    public float maxCooldown = 15f;
    public float resetCooldown = 0f;
    public float currentCooldown;
    public Vector3 Offset;

    private void Start() 
    {
        currentCooldown = resetCooldown;
        slider.value = currentCooldown;
        slider.maxValue = maxCooldown;
    }

    private void Update() 
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);

        if (currentCooldown <= 15 && !enemy.canUseSpecialAttack)
        {
            currentCooldown += Time.deltaTime;
        }
        else if (currentCooldown >= 15 && !enemy.canUseSpecialAttack)
        {
            currentCooldown = resetCooldown;
        }
        slider.value = currentCooldown;

        if (currentCooldown >= maxCooldown && enemy.canUseSpecialAttack)
        {
            slider.gameObject.SetActive(false);
        }
    }
}
