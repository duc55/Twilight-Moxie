using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUberMode : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Weapon meleeWeapon;
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] GameObject normalModel;
    [SerializeField] GameObject uberModel;


    public void ActivateUberMode()
    {
        health.maxHitPoints = 100;
        health.Heal(100);

        meleeWeapon.transform.localScale *= 2f;
        meleeWeapon.SetDamage(5, 10);
        meleeWeapon.GetComponent<Animator>().SetFloat("AttackSpeed", 2.0f);
        playerAttack.timeBetweenAttacks = 0.6f;

        normalModel.SetActive(false);
        uberModel.SetActive(true);
    }
}
