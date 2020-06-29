using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Inctance = null;

    [SerializeField] Image _healthBar, _protectionBar, _speedBar, _damageBar;
    [SerializeField] Text _specializationNameText;

    private void Start()
    {
        if (Inctance == null)
            Inctance = this;
        else
            Destroy(gameObject);
    }

    public void MainParametrsPanel(int health, int protection, int damage, float walkingSpeed, float runningSpeed, string specializationName)
    {
        _healthBar.fillAmount = (float)health / 10000;
        _protectionBar.fillAmount = (float)protection / 100;
        _damageBar.fillAmount = (float)damage / 10000;
        _speedBar.fillAmount = (walkingSpeed + runningSpeed) / 20;

        _specializationNameText.text = specializationName;
    }

    public void StartGame()
    {
        GameManager.Inctance.Player = Instantiate(GameManager.Inctance.Player, Vector3.zero, Quaternion.identity);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
