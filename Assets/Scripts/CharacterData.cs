using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public string CharacterName;

    public StatSystem Stats;

    [HideInInspector]
    public float basicAttackCooldownInternal, spell_1CooldownInternal, spell_2CooldownInternal, spell_3CooldownInternal, spell_4CooldownInternal;


    public bool CanSpell_1
    {
        get { return spell_1CooldownInternal <= 0.0f; }
    }

    public bool CanSpell_2
    {
        get { return spell_2CooldownInternal <= 0.0f; }
    }

    public bool CanSpell_3
    {
        get { return spell_3CooldownInternal <= 0.0f; }
    }

    public bool CanSpell_4
    {
        get { return spell_4CooldownInternal <= 0.0f; }
    }

    public bool CanAttack
    {
        get { return basicAttackCooldownInternal <= 0.0f; }
    }

    public void Init()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Stats.Init(this);
        basicAttackCooldownInternal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (basicAttackCooldownInternal > 0.0f)
            basicAttackCooldownInternal -= Time.deltaTime;

        if (spell_1CooldownInternal > 0.0f)
        {
            spell_1CooldownInternal -= Time.deltaTime;
        }
        if (spell_2CooldownInternal > 0.0f)
        {
            spell_2CooldownInternal -= Time.deltaTime;
        }
        if (spell_3CooldownInternal > 0.0f)
        {
            spell_3CooldownInternal -= Time.deltaTime;
        }
        if (spell_4CooldownInternal > 0.0f)
        {
            spell_4CooldownInternal -= Time.deltaTime;
        }
    }

    public bool CanAttackReach(CharacterData target, float range)
    {
        float distance = Vector3.SqrMagnitude(transform.position - target.transform.position);
        if (distance < range && target != null)
        {
            return true;
        }

        return false;
    }

    public void GetDamaged(CharacterData damageDealer, CharacterData damageReceiver, int amount)
    {
        float armorDamageMultiplier;
        if (damageReceiver.Stats.baseStats.armor >= 0)
        {
            armorDamageMultiplier = 100.0f / (100.0f + damageReceiver.Stats.baseStats.armor);
        }
        else
        {
            armorDamageMultiplier = 2.0f - (100.0f / (100.0f - damageReceiver.Stats.baseStats.armor));
        }
        float damage = amount * armorDamageMultiplier;
        Debug.Log(damageDealer.name + " hit " + damageReceiver.name + " for " + Mathf.RoundToInt(damage).ToString() + " damage");
        Stats.ChangeHealth(Mathf.RoundToInt(damage));

        // TODO add calculation for critical strikes
    }
}
