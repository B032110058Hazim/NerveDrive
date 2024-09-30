using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class Pole : MonoBehaviour
{
    [SerializeField] private UnityEvent poleHit = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        poleHit.Invoke();
        Destroy(gameObject);
    }
}
