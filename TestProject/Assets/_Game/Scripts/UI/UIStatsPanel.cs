using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStatsPanel : MonoBehaviour
{
    public static UIStatsPanel Inctance = null;

    [SerializeField] Image _healthBar, _protectionBar, _speedBar, _damageBar;
    [SerializeField] Text _specializationNameText;

    private void Start()
    {
        if (Inctance == null)
            Inctance = this;
        else
            Destroy(gameObject);
    }

    public void StatsPanel(CharacterStats characterStats, WeaponStats weaponStats)
    {
        _healthBar.fillAmount = (float)characterStats.Health / 10000;
        _protectionBar.fillAmount = (float)characterStats.Protection / 500;
        _damageBar.fillAmount = (float)weaponStats.Damage / 5000;
        _speedBar.fillAmount = (characterStats.WalkingSpeed + characterStats.RunningSpeed) / 20;

        _specializationNameText.text = characterStats.SpecializationName;
    }

    public void StartGame()
    {
        GameManager.Inctance.Player = Instantiate(GameManager.Inctance.Player, Vector3.zero, Quaternion.identity);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
