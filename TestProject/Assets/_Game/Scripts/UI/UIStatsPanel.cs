using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStatsPanel : MonoBehaviour
{
    public static UIStatsPanel Inctance = null;

    [SerializeField] Image _healthBar, _protectionBar, _speedBar, _damageBar;
    [SerializeField] Text _specializationNameText;

    CharacterStats characterStats;
    WeaponStats weaponStats;

    private void Start()
    {
        if (Inctance == null)
            Inctance = this;
        else
            Destroy(gameObject);
    }

    public void StatsPanel(CharacterStats characterStats, WeaponStats weaponStats)
    {
        this.characterStats = characterStats;
        this.weaponStats = weaponStats;

        _healthBar.fillAmount = (float)this.characterStats.Health / 10000;
        _protectionBar.fillAmount = (float)this.characterStats.Protection / 500;
        _damageBar.fillAmount = (float)weaponStats.Damage / 5000;
        _speedBar.fillAmount = (this.characterStats.WalkingSpeed + this.characterStats.RunningSpeed) / 20;

        _specializationNameText.text = this.characterStats.SpecializationName;
    }

    public void StartGame()
    {
        GameManager.Inctance.Player = Instantiate(GameManager.Inctance.Player, Vector3.zero, Quaternion.identity);
        GameManager.Inctance.Player.GetComponent<Character>().Setup(characterStats);
        GameManager.Inctance.Player.GetComponentInChildren<Weapon>().Setup(weaponStats);
    }
}
