using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDamage : MonoBehaviour
{

    [SerializeField]
    protected Door door;
    [SerializeField]
    protected float damage;

    protected GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != player || door.DoorState != Door.State.CLOSING)
        {
            return;
        }
        player.SendMessage("Damage", damage);
    }
}
