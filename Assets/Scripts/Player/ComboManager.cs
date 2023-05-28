using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public int comboCount;
    public float comboTimer;
    public float maxComboTime = 2f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else
        {
            ResetCombo();
        }
    }

    public void IncreaseCombo()
    {
        comboCount++;
        comboTimer = maxComboTime;
        // Add logic to display combo text or effects
    }

    public void ResetCombo()
    {
        comboCount = 0;
        // Add logic to reset combo text or effects
    }
}
