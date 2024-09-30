using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelect;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject data;

    [SerializeField] private TMP_Dropdown steerLeftCommand;
    [SerializeField] private TMP_Dropdown steerRightCommand;
    [SerializeField] private TMP_Dropdown gasCommand;
    [SerializeField] private TMP_Dropdown brakeCommand;
    [SerializeField] private TMP_Dropdown higherSelectionCommand;
    [SerializeField] private TMP_Dropdown lowerSelectionCommand;
    [SerializeField] private TMP_Dropdown swapCommand;

    [SerializeField] private TMP_Text totalTimeTaken;
    [SerializeField] private TMP_Text hillClimbTimeTaken;
    [SerializeField] private TMP_Text parallelParkingTimeTaken;
    [SerializeField] private TMP_Text threeCornersTimeTaken;
    [SerializeField] private TMP_Text sCurveTimeTaken;
    [SerializeField] private TMP_Text zCurveTimeTaken;
    [SerializeField] private TMP_Text rampTimeTaken;

    [SerializeField] private TMP_Text hillClimbStopped;
    [SerializeField] private TMP_Text threeCorners1Stopped;
    [SerializeField] private TMP_Text threeCorners2Stopped;
    [SerializeField] private TMP_Text rampStopped;

    [SerializeField] private TMP_Text parallel;

    // Start is called before the first frame update
    void Start()
    {
        if(Test.totalTime > 0)
        {
            totalTimeTaken.text = Mathf.Floor(Test.totalTime / 60).ToString("00") + ":" + Mathf.FloorToInt(Test.totalTime % 60).ToString("00");
            hillClimbTimeTaken.text = Mathf.Floor(Test.hillClimbTime / 60).ToString("00") + ":" + Mathf.FloorToInt(Test.hillClimbTime % 60).ToString("00");
            parallelParkingTimeTaken.text = Mathf.Floor(Test.parallelParkingTime / 60).ToString("00") + ":" + Mathf.FloorToInt(Test.parallelParkingTime % 60).ToString("00");
            threeCornersTimeTaken.text = Mathf.Floor(Test.threeCornersTime / 60).ToString("00") + ":" + Mathf.FloorToInt(Test.threeCornersTime % 60).ToString("00");
            sCurveTimeTaken.text = Mathf.Floor(Test.sCurveTime / 60).ToString("00") + ":" + Mathf.FloorToInt(Test.sCurveTime % 60).ToString("00");
            zCurveTimeTaken.text = Mathf.Floor(Test.zCurveTime / 60).ToString("00") + ":" + Mathf.FloorToInt(Test.zCurveTime % 60).ToString("00");
            rampTimeTaken.text = Mathf.Floor(Test.rampTime / 60).ToString("00") + ":" + Mathf.FloorToInt(Test.rampTime % 60).ToString("00");

            hillClimbStopped.text = Test.hillClimbStop ? "T" : "F";
            threeCorners1Stopped.text = Test.threeCornersStop ? "T" : "F";
            threeCorners2Stopped.text = Test.threeCornersStop2 ? "T" : "F";
            rampStopped.text = Test.rampStop ? "T" : "F";

            parallel.text = Test.parallelParkingStop ? "T" : "F";

            Data();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
        options.SetActive(false);
        data.SetActive(false);
    }

    public void LevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        options.SetActive(false);
        data.SetActive(false);
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        options.SetActive(true);
        data.SetActive(false);
    }

    public void Data()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        options.SetActive(false);
        data.SetActive(true);
    }

    public void HillClimb()
    {

    }

    public void ParallelParking()
    {

    }

    public void ThreeCorners()
    {

    }

    public void Ramp()
    {

    }

    public void SCurve()
    {

    }

    public void ZCurve()
    {

    }

    public void Play()
    {
        CarController.steerLeftCommand = dropdownValue(steerLeftCommand.value);
        CarController.steerRightCommand = dropdownValue(steerRightCommand.value);
        CarController.gasCommand = dropdownValue(gasCommand.value);
        CarController.brakeCommand = dropdownValue(brakeCommand.value);
        CarController.higherSelectionCommand = dropdownValue(higherSelectionCommand.value);
        CarController.lowerSelectionCommand = dropdownValue(lowerSelectionCommand.value);
        CarController.swapCommand = dropdownValue(swapCommand.value);

        SceneManager.LoadScene("DrivingCircuit");
    }

    public void Exit()
    {
        //EditorApplication.ExitPlaymode();
    }

    private string dropdownValue(int x)
    {
        switch (x)
        {
            case 0:
                return "neutral";
            case 1:
                return "push";
            case 2:
                return "pull";
            case 3:
                return "lift";
            case 4:
                return "drop";
            case 5:
                return "left";
            case 6:
                return "right";
            case 7:
                return "rotateLeft";
            case 8:
                return "rotateRight";
            case 9:
                return "rotateClockwise";
            case 10:
                return "rotateCounterClockwise";
            case 11:
                return "rotateForwards";
            case 12:
                return "rotateReverse";
            case 13:
                return "disappear";
            default:
                throw new NotImplementedException("Outside of dropdown range");
        }
    }
}
