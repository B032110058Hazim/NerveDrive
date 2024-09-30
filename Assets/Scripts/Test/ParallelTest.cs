using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParallelTest : MonoBehaviour
{
    private bool evaluating;

    [SerializeField] private CarController carController;
    [SerializeField] private UnityEvent setMark = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (evaluating && carController.mode == CarController.TransmissionMode.Parking)
        {
            float mark = Mathf.Lerp(0, 4, Mathf.InverseLerp(0, 360, carController.transform.eulerAngles.y));//Mathf.Abs(Mathf.DeltaAngle(carController.transform.rotation.y, 90));
            mark = Mathf.Round(mark);
            if(mark == 1 || mark == 3)
                setMark.Invoke();
            evaluating = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        evaluating = true;
    }

    private void OnTriggerExit(Collider other)
    {
        evaluating = false;
    }
}
