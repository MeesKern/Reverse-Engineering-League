using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[System.Serializable]
public class StatSystem
{
    [System.Serializable]
    public class Stats
    {
        public int health;
        public int mana;
        public int abilityPower;
        public int attackDamage;
        public float attackSpeed;
        public int armor;
        public int magicResistance;
        public float cooldownReduction;
        public int experience;
        public float goldPerSecond;
        public int movementSpeed;
        public int attackRange;
        public int projectileSpeed;
        public float spell1_Cooldown;
        public float spell2_Cooldown;
        public float spell3_Cooldown;
        public float spell4_Cooldown;

        public void Copy(Stats other)
        {
            health = other.health;
            mana = other.mana;
            abilityPower = other.abilityPower;
            attackDamage = other.attackDamage;
            attackSpeed = other.attackSpeed;
            armor = other.armor;
            magicResistance = other.magicResistance;
            cooldownReduction = other.cooldownReduction;
            experience = other.experience;
            goldPerSecond = other.goldPerSecond;
            movementSpeed = other.movementSpeed;
            attackRange = other.attackRange;
            projectileSpeed = other.projectileSpeed;
            spell1_Cooldown = other.spell1_Cooldown;
            spell2_Cooldown = other.spell2_Cooldown;
            spell3_Cooldown = other.spell3_Cooldown;
            spell4_Cooldown = other.spell4_Cooldown;
        }


    }


    public Stats baseStats;
    public Stats stats { get; set; } = new Stats();


    public int currentHealth { get; private set; }
    public int currentMana { get; private set; }

    CharacterData owner;

    public void Init(CharacterData statsOwner)
    {
        stats.Copy(baseStats);
        currentHealth = stats.health;
        currentMana = stats.mana;
        owner = statsOwner;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, stats.health);
    }

    public void ChangeMana(int amount)
    {
        currentMana = Mathf.Clamp(currentMana + amount, 0, stats.mana);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(StatSystem.Stats))]
public class StatsDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int enumTypesCount = 12;
        int lineCount = enumTypesCount + 7;
        float extraHeight = 6f;
        float propertyHeight = lineCount * EditorGUIUtility.singleLineHeight + extraHeight;

        return propertyHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;

        var currentRect = position;
        currentRect.height = EditorGUIUtility.singleLineHeight;

        EditorGUI.DropShadowLabel(currentRect, property.displayName);

        currentRect.y += EditorGUIUtility.singleLineHeight + 6f;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.health)));

        currentRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.mana)));

        currentRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.abilityPower)));

        currentRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.attackDamage)));

        currentRect.y += EditorGUIUtility.singleLineHeight + 6f;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.attackSpeed)));

        currentRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.armor)));

        currentRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.magicResistance)));

        currentRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.cooldownReduction)));

        currentRect.y += EditorGUIUtility.singleLineHeight + 6f;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.experience)));

        currentRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.goldPerSecond)));

        currentRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.movementSpeed)));

        currentRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.attackRange)));

        currentRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.projectileSpeed)));

        currentRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.spell1_Cooldown)));
        currentRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.spell2_Cooldown)));
        currentRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.spell3_Cooldown)));
        currentRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(currentRect, property.FindPropertyRelative(nameof(StatSystem.Stats.spell4_Cooldown)));

        EditorGUI.EndProperty();
    }
}
#endif
