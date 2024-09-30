using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform xrOriginTransform;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject ray1;
    [SerializeField] private GameObject ray2;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartLevel()
    {
        canvas.SetActive(false);
        xrOriginTransform.localPosition = new Vector3(0.42f, -0.8f, 0.3f);
        xrOriginTransform.localRotation = Quaternion.identity;
    }
}
