using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [Header("Main parameters")]
    [SerializeField] private float _walkingSpeed;
    [SerializeField] private float _runningSpeed;

    public float WalkingSpeed { get { return _walkingSpeed; } private set { _walkingSpeed = value; } }
    public float RunningSpeed { get { return _runningSpeed; } private set { _runningSpeed = value; } }
}
