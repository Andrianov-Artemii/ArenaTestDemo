using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = new Vector3(0, 8, -4);

    void FixedUpdate()
    {
        if (GameManager.Inctance.Player != null)
        {
            Vector3 smoothPosition = Vector3.Lerp(transform.position, GameManager.Inctance.Player.transform.position + _offset, 0.125f);
            transform.position = smoothPosition;
        }
    }
}
