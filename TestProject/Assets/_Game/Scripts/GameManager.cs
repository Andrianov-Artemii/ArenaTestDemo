using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inctance = null;

    [SerializeField] private FixedJoystick _walkingJoystick;
    [SerializeField] private FloatingJoystick _atackingJoystick;

    private GameObject player;

    public FixedJoystick WalkingJoystick { get { return _walkingJoystick; } }
    public FloatingJoystick AtackingJoystick { get { return _atackingJoystick; } }

    public GameObject Player { get { return player; } set { player = value; } }

    private void Start()
    {
        if (Inctance == null)
            Inctance = this;
        else
            Destroy(gameObject);
    }
}
