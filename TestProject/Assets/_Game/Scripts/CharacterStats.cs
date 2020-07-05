using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Assets/Character", order = 1)]
public class CharacterStats : ScriptableObject
{
    public string SpecializationName;
    public GameObject Character;

    public int Health;
    public int Protection;
    public float WalkingSpeed;
    public float RunningSpeed;
}

[CreateAssetMenu(fileName = "Weapon", menuName = "Assets/Weapon", order = 2)]
public class WeaponStats : ScriptableObject
{
    public int Damage;
    public int MaxAttackCount;

    public float AttackRange;
    public float ReloadTime;
    public float AttackDelay;
}