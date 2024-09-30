using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class Test : MonoBehaviour
{

    [SerializeField] GameObject hillClimbIcon;
    [SerializeField] GameObject parallelParkingIcon;
    [SerializeField] GameObject threeCornersIcon;
    [SerializeField] GameObject rampIcon;
    [SerializeField] GameObject sCurveIcon;
    [SerializeField] GameObject zCurveIcon;

    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform hillClimbStart;
    [SerializeField] Transform parallelParkingStart;
    [SerializeField] Transform threeCornersStart;
    [SerializeField] Transform rampStart;
    [SerializeField] Transform sCurveStart;
    [SerializeField] Transform zCurveStart;

    [SerializeField] GameObject pole;
    [SerializeField] GameObject cones;

    public static float totalTime;

    public static float hillClimbTime;
    public static float parallelParkingTime;
    public static float threeCornersTime;
    public static float rampTime;
    public static float sCurveTime;
    public static float zCurveTime;

    public static bool stopStop;
    public static bool hillClimbStop;
    public static bool threeCornersStop;
    public static bool threeCornersStop2;
    public static bool rampStop;
    public static bool parallelParkingStop;

    public static bool poleHit;

    private List<string> evaluation = new List<string>();

    public void SetHillClimbTime(float t)
    {
        hillClimbTime = t;
        nextDestination();
    }

    public void SetParallelParkingTime(float t)
    {
        parallelParkingTime = t;
        nextDestination();
    }

    public void SetThreeCornersTime(float t)
    {
        threeCornersTime = t;
        nextDestination();
    }

    public void SetRampTime(float t)
    {
        rampTime = t;
        nextDestination();
    }

    public void SetSCurveTime(float t)
    {
        sCurveTime = t;
        nextDestination();
    }

    public void SetZCurveTime(float t)
    {
        zCurveTime = t;
        nextDestination();
    }

    public void SetStopStop()
    {
        stopStop = true;
    }

    public void SetHillClimbStop()
    {
        hillClimbStop = true;
    }

    public void SetThreeCornersStop()
    {
        threeCornersStop = true;
    }

    public void SetThreeCornersStop2()
    {
        threeCornersStop2 = true;
    }

    public void SetRampStop()
    {
        rampStop = true;
        if(pole != null) Destroy(pole);
    }

    public void SetPoleHit()
    {
        poleHit = true;
    }

    public void SetParallelParkingStop()
    {
        parallelParkingStop = true;
    }

    public void nextDestination()
    {
        if (hillClimbTime > 0f)
        {
            agent.SetDestination(parallelParkingStart.position);
            hillClimbIcon.SetActive(false);
            parallelParkingIcon.SetActive(true);
            threeCornersIcon.SetActive(false);
            rampIcon.SetActive(false);
            sCurveIcon.SetActive(false);
            zCurveIcon.SetActive(false);
        }

        if (hillClimbTime > 0f && parallelParkingTime > 0f)
        {
            agent.SetDestination(threeCornersStart.position);
            hillClimbIcon.SetActive(false);
            parallelParkingIcon.SetActive(false);
            threeCornersIcon.SetActive(true);
            rampIcon.SetActive(false);
            sCurveIcon.SetActive(false);
            zCurveIcon.SetActive(false);
        }

        if (hillClimbTime > 0f && parallelParkingTime > 0f && threeCornersTime > 0f)
        {
            agent.SetDestination(rampStart.position);
            hillClimbIcon.SetActive(false);
            parallelParkingIcon.SetActive(false);
            threeCornersIcon.SetActive(false);
            rampIcon.SetActive(true);
            sCurveIcon.SetActive(false);
            zCurveIcon.SetActive(false);
        }

        if (hillClimbTime > 0f && parallelParkingTime > 0f && threeCornersTime > 0f && rampTime > 0f)
        {
            agent.SetDestination(sCurveStart.position);
            hillClimbIcon.SetActive(false);
            parallelParkingIcon.SetActive(false);
            threeCornersIcon.SetActive(false);
            rampIcon.SetActive(false);
            sCurveIcon.SetActive(true);
            zCurveIcon.SetActive(false);
        }

        if (hillClimbTime > 0f && parallelParkingTime > 0f && threeCornersTime > 0f && rampTime > 0f && sCurveTime > 0f)
        {
            agent.SetDestination(zCurveStart.position);
            hillClimbIcon.SetActive(false);
            parallelParkingIcon.SetActive(false);
            threeCornersIcon.SetActive(false);
            rampIcon.SetActive(false);
            sCurveIcon.SetActive(false);
            zCurveIcon.SetActive(true);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(hillClimbStart.position);
    }

    // Update is called once per frame
    void Update()
    {
        nextDestination();
        totalTime += Time.deltaTime;

        if(hillClimbTime > 0f && parallelParkingTime > 0f && threeCornersTime > 0f && rampTime > 0f && sCurveTime > 0f && zCurveTime > 0f)
        {

            SceneManager.LoadScene("MainMenu");
        }
    }
}
