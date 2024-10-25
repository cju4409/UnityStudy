using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour, IBattle
{
    public UnityAction deathAlarm { get; set; }
    public bool IsLive { get => true; }
    public void OnDamage(float dmg)
    {
        Destroy(gameObject);
    }
}
