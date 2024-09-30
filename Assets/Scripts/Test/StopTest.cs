using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopTest : MonoBehaviour
{
    private bool evaluating;
    private Rigidbody rb;

    [SerializeField] private UnityEvent setStop = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (evaluating && rb.velocity.z < 0.1f && rb.velocity.z > -0.1f)
        {
            setStop.Invoke();
            evaluating = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        evaluating = true;
        rb = other.attachedRigidbody;
    }

    private void OnTriggerExit(Collider other)
    {
        evaluating = false;
    }
}
