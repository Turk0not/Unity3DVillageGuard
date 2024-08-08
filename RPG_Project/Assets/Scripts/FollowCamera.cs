using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Transform player;

    void Update()
    {
        transform.position = player.position;
    }
}
