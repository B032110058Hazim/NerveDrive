using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeTest : MonoBehaviour
{
    private float time;
    private bool evaluating;

    [SerializeField] private UnityEvent<float> setTime = new UnityEvent<float>();
    [SerializeField] private UnityEvent hidePath = new UnityEvent();
    [SerializeField] private UnityEvent showPath = new UnityEvent();

    [SerializeField] private BoxCollider coll;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (evaluating)
            time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != coll) return;
        hidePath.Invoke();
        evaluating = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != coll) return;
        setTime.Invoke(time);
        showPath.Invoke();
    }
}
