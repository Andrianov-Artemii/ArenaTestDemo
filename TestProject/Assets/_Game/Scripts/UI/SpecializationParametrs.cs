using UnityEngine;

public class SpecializationParametrs : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private string _specializationName;

    private int _health, _protection, _damage;
    private float _walkingSpeed, _runningSpeed;

    private void Setup()
    {
        PlayerController playerParametrs;
        HealthManager healthManager;
        Weapons weapons;

        playerParametrs = _player.GetComponent<PlayerController>();
        healthManager = _player.GetComponent<HealthManager>();
        weapons = playerParametrs.GetComponentInChildren<Weapons>();

        _health = healthManager.Health;
        _protection = healthManager.Protection;
        _damage = weapons.Damage;
        _walkingSpeed = playerParametrs.WalkingSpeed;
        _runningSpeed = playerParametrs.RunningSpeed;
    }

    public void ChoiceSpecialization()
    {
        Setup();

        MenuManager.Inctance.MainParametrsPanel(_health, _protection, _damage, _walkingSpeed, _runningSpeed, _specializationName);
        GameManager.Inctance.Player = _player;
    }
}
