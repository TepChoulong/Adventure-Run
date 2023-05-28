using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttackController : MonoBehaviour
{
    public ComboAttack[] comboAttacks;
    private Animator animator;
    private ComboManager comboManager;
    private int comboIndex;

    void Start()
    {
        animator = GetComponent<Animator>();
        comboManager = GetComponent<ComboManager>();
        comboIndex = 0;
    }

    void PerformNextComboAttack()
    {
        if (comboIndex < comboAttacks.Length - 1)
        {
            ComboAttack attack = comboAttacks[comboIndex + 1];
            animator.Play(attack.animation.name);
            comboManager.IncreaseCombo();
            comboIndex++;
        }
        else
        {
            comboManager.ResetCombo();
            comboIndex = 0;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (comboIndex < comboAttacks.Length)
            {
                ComboAttack attack = comboAttacks[comboIndex];
                animator.Play(attack.animation.name);
                comboManager.IncreaseCombo();
                comboIndex++;
            }
            else
            {
                comboManager.ResetCombo();
                comboIndex = 0;
            }
        }
    }
}
