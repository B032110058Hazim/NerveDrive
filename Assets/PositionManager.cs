using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    [SerializeField] Transform playerPosition;

    [SerializeField] Transform hillClimbPosition;
    [SerializeField] Transform ParallelParkingPosition;
    [SerializeField] Transform ThreeCornersPosition;
    [SerializeField] Transform RampPosition;
    [SerializeField] Transform SCurvePosition;
    [SerializeField] Transform ZCurvePosition;
    [SerializeField] Transform CarPosition;

    // Start is called before the first frame update
    void Start()
    {
        MoveToHillClimb();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToHillClimb()
    {
        playerPosition.position = hillClimbPosition.position;
        playerPosition.rotation = hillClimbPosition.rotation;
    }

    public void MoveToParallelParking()
    {
        playerPosition.position = ParallelParkingPosition.position;
        playerPosition.rotation = ParallelParkingPosition.rotation;
    }

    public void MoveToRamp()
    {
        playerPosition.position = RampPosition.position;
        playerPosition.rotation = RampPosition.rotation;
    }

    public void MoveToThreeCorners()
    {
        playerPosition.position = ThreeCornersPosition.position;
        playerPosition.rotation = ThreeCornersPosition.rotation;
    }

    public void MoveToSCurve()
    {
        playerPosition.position = SCurvePosition.position;
        playerPosition.rotation = SCurvePosition.rotation;
    }

    public void MoveToZCurve()
    {
        playerPosition.position = ZCurvePosition.position;
        playerPosition.rotation = ZCurvePosition.rotation;
    }

    public void MoveToCar()
    {
        playerPosition.position = CarPosition.position;
        playerPosition.rotation = CarPosition.rotation;
    }
}
