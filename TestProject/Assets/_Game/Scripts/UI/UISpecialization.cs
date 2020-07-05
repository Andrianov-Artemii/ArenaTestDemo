using UnityEngine;

public class UISpecialization : MonoBehaviour
{
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private WeaponStats _weaponStats;

    public void ChoiceSpecialization()
    {
        UIStatsPanel.Inctance.StatsPanel(_characterStats, _weaponStats);
        GameManager.Inctance.Player = _characterStats.Character;
    }
}
