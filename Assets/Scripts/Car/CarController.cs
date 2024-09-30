using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;
using static UnityEngine.GraphicsBuffer;

public class CarController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] InputActionReference gasInputMap;
    [SerializeField] InputActionReference brakeInputMap;
    [SerializeField] InputActionReference resetInputMap;

    [SerializeField] XRKnob steeringWheel;
    //[SerializeField] XRLever stickShift;

    [Header("Physics")]
    [SerializeField] private Rigidbody chassisRB;
    [SerializeField] private Transform centerOfMass;

    [SerializeField] private WheelColliders wheelColliders;
    [SerializeField] private WheelMeshes wheelMeshes;

    [Header("Car Specifications")]
    [SerializeField] private float horsepower;
    [SerializeField] private float idleRPM;
    [SerializeField] private float redLine;
    [SerializeField] private float wheelBase;
    [SerializeField] private float rearTrack;
    [SerializeField] private float turnRadius;
    [SerializeField] private float differentialRatio;
    [SerializeField] private float[] gearRatios;
    [SerializeField] private float[] upshiftRPM;
    [SerializeField] private float[] downshiftRPM;

    [Header("Curves")]
    [SerializeField] private AnimationCurve brakePower;
    [SerializeField] private AnimationCurve downForcePower;
    [SerializeField] private AnimationCurve HPRPMRatio;

    [Header("Instrument Cluster")]
    [SerializeField] private Transform RPMHand;
    [SerializeField] private Transform KPHHand;
    [SerializeField] private TextMeshProUGUI textMode;

    [Header("Screen")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private Transform startPos;

    private int gear;
    private float clutch = 1;
    private float RPM;
    public TransmissionMode mode = TransmissionMode.Drive;

    private float gasInput, steerInput, brakeInput;

    public static string steerLeftCommand;
    public static string steerRightCommand;

    public static string gasCommand;
    public static string brakeCommand;

    public static string higherSelectionCommand;
    public static string lowerSelectionCommand;

    public static string swapCommand;

    //private bool steer = true;
    private string lastCommand;

    public List<string> transforms = new List<string>();

    private void transformUpdate()
    {
        return;
        transforms.Add(transform.position.x.ToString());
        transforms.Add(transform.position.y.ToString());
        transforms.Add(transform.position.z.ToString());

        transforms.Add(transform.eulerAngles.x.ToString());
        transforms.Add(transform.eulerAngles.y.ToString());
        transforms.Add(transform.eulerAngles.z.ToString());
    }

    public void writeToFile()
    {
        File.WriteAllLines("C:\\Users\\Name\\Documents\\NerveDrive\\Assets\\Data\\Default\\S Curve.txt", transforms, Encoding.UTF8);
    }

    void Start()
    {
        chassisRB.centerOfMass = centerOfMass.localPosition;

        lineRenderer.startWidth = 2.15f;
        lineRenderer.endWidth = 2.15f;
        lineRenderer.positionCount = 0;

        InvokeRepeating("transformUpdate", 2f, 0.03f);
    }

    void Update()
    {
        RPMHand.localEulerAngles = new Vector3(RPMHand.localEulerAngles.x, Mathf.Lerp(-110, 110, RPM / 8000), 0.25f);

        float KPH = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;

        KPHHand.localEulerAngles = new Vector3(RPMHand.localEulerAngles.x, Mathf.Lerp(-110, 110, KPH / 220), 0.25f);

        //DrawPath();

        if (Input.GetKeyDown(KeyCode.P))
        {
            writeToFile();
        }
    }

    void FixedUpdate()
    {
        //KBInput();
        VRInput();
        //BCIInput();

        switch (mode)
        {
            case TransmissionMode.Parking:
                Parking();
                textMode.text = "P";
                break;
            case TransmissionMode.Reverse:
                Steer();
                Brake();
                Reverse();
                textMode.text = "R";
                break;
            case TransmissionMode.Neutral:
                Brake();
                Neutral();
                textMode.text = "N";
                break;
            case TransmissionMode.Drive:
                Steer();
                Drive();
                Brake();
                DownForce();
                AntiRollBar();
                textMode.text = "D";
                break;
            case TransmissionMode.Gear3:
                Steer();
                Gear3();
                Brake();
                DownForce();
                AntiRollBar();
                textMode.text = "3";
                break;
            case TransmissionMode.Gear2:
                Steer();
                Gear2();
                Brake();
                DownForce();
                AntiRollBar();
                textMode.text = "2";
                break;
            default:
                throw new NotImplementedException("Unimplemented transmission mode");
        }

        AnimateWheels();
    }

    public void SetTransmissionMode(int i)
    {
        mode = (TransmissionMode)i;
    }

    private void KBInput()
    {
        gasInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
        brakeInput = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;
    }

    private void VRInput()
    {
        gasInput = gasInputMap.action.ReadValue<float>();
        brakeInput = brakeInputMap.action.ReadValue<float>();
        steerInput = Mathf.Lerp(-1, 1, steeringWheel.value);
        if (resetInputMap.action.IsPressed())
            goToStart();
    }

    private void BCIInput()
    {
        if (BCI.action == "push")//gasCommand
            gasInput = (float)BCI.power;

        if (BCI.action == "pull")//brakeCommand
            brakeInput = 1.0f;

        if (BCI.action == "left")//steerLeftCommand
            steerInput = Mathf.Lerp(0.0f, -1.0f, (float)BCI.power);

        if (BCI.action == "right")//steerRightCommand
            steerInput = Mathf.Lerp(0.0f, 1.0f, (float)BCI.power);

        if (chassisRB.velocity.z < 0.1f && chassisRB.velocity.z > -0.1f)
        {

            if (BCI.action == "left" && lastCommand == "neutral")//steerLeftCommand
            {
                SetTransmissionMode(Math.Max(0, (int)mode - 1));
                Debug.Log("Negative Set Transmission");
            }


            if (BCI.action == "right" && lastCommand == "neutral")//steerRightCommand
            {
                SetTransmissionMode(Math.Min(5, (int)mode + 1));
                Debug.Log("Positive Set Transmission");
            }
        }

        lastCommand = BCI.action;
    }

    private void goToStart()
    {
        transform.position = startPos.transform.position;
        transform.rotation = startPos.transform.rotation;
    }

        private void Parking()
    {
        wheelColliders.FRWheel.brakeTorque = wheelColliders.FLWheel.brakeTorque = wheelColliders.RRWheel.brakeTorque = wheelColliders.RLWheel.brakeTorque = Mathf.Infinity;
    }

    private void Reverse()
    {
        float wheelRPM = Mathf.Abs((wheelColliders.RRWheel.rpm + wheelColliders.RLWheel.rpm) / 2f) * gearRatios[gear] * differentialRatio;
        RPM = Mathf.Max(idleRPM, wheelRPM);
        float torque = (HPRPMRatio.Evaluate(RPM / redLine) * horsepower / RPM) * gearRatios[gear] * differentialRatio * 5252f * clutch;

        wheelColliders.FRWheel.motorTorque = wheelColliders.FLWheel.motorTorque = -torque * gasInput;
    }

    private void Neutral()
    {
        float wheelRPM = Mathf.Abs((wheelColliders.RRWheel.rpm + wheelColliders.RLWheel.rpm) / 2f) * gearRatios[gear] * differentialRatio;
        RPM = Mathf.Max(idleRPM, wheelRPM);
    }

    private void Gear3()
    {
        gear = 3;

        float wheelRPM = Mathf.Abs((wheelColliders.RRWheel.rpm + wheelColliders.RLWheel.rpm) / 2f) * gearRatios[gear] * differentialRatio;
        RPM = Mathf.Max(idleRPM, wheelRPM);
        float torque = (HPRPMRatio.Evaluate(RPM / redLine) * horsepower / RPM) * gearRatios[gear] * differentialRatio * 5252f * clutch;

        wheelColliders.FRWheel.motorTorque = wheelColliders.FLWheel.motorTorque = torque * gasInput;
    }

    private void Gear2()
    {
        gear = 2;

        float wheelRPM = Mathf.Abs((wheelColliders.RRWheel.rpm + wheelColliders.RLWheel.rpm) / 2f) * gearRatios[gear] * differentialRatio;
        RPM = Mathf.Max(idleRPM, wheelRPM);
        float torque = (HPRPMRatio.Evaluate(RPM / redLine) * horsepower / RPM) * gearRatios[gear] * differentialRatio * 5252f * clutch;

        wheelColliders.FRWheel.motorTorque = wheelColliders.FLWheel.motorTorque = torque * gasInput;
    }

    private void Drive()
    {
        switch (gear)
        {
            case 0:
                if (RPM > upshiftRPM[0])
                    gear++;
                break;
            case 1:
                if (RPM > upshiftRPM[1])
                    gear++;
                if (RPM < downshiftRPM[0])
                    gear--;
                break;
            case 2:
                if (RPM > upshiftRPM[2])
                    gear++;
                if (RPM < downshiftRPM[1])
                    gear--;
                break;
            case 3:
                if (RPM < downshiftRPM[2])
                    gear--;
                break;
            default:
                throw new NotImplementedException("Unimplemented gear");
        }
        //5252f is approximately equal to (1 horsepower) / (1 revolution per minute)
        float wheelRPM = Mathf.Abs((wheelColliders.RRWheel.rpm + wheelColliders.RLWheel.rpm) / 2f) * gearRatios[gear] * differentialRatio;
        RPM = Mathf.Max(idleRPM, wheelRPM);
        float torque = (HPRPMRatio.Evaluate(RPM / redLine) * horsepower / RPM) * gearRatios[gear] * differentialRatio * 5252f * clutch;

        wheelColliders.FRWheel.motorTorque = wheelColliders.FLWheel.motorTorque = torque * gasInput;
    }

    private void Steer()
    {
        wheelColliders.FRWheel.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2 * ((steerInput > 0) ? 1 : -1)))) * steerInput;
        wheelColliders.FLWheel.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2 * ((steerInput > 0) ? -1 : 1)))) * steerInput;
    }

    private void Brake()
    {
        wheelColliders.FRWheel.brakeTorque = wheelColliders.FLWheel.brakeTorque = brakePower.Evaluate(brakeInput) * 0.7f;
        wheelColliders.RRWheel.brakeTorque = wheelColliders.RLWheel.brakeTorque = brakePower.Evaluate(brakeInput) * 0.3f;
    }

    private void DownForce()
    {
        chassisRB.AddForce(-transform.up * downForcePower.Evaluate(chassisRB.velocity.magnitude));
    }

    private void AntiRollBar()
    {
        WheelHit hit;
        float travelL;
        float travelR;

        if (isGrounded())
        {
            bool groundedL = wheelColliders.FLWheel.GetGroundHit(out hit), groundedR = wheelColliders.FRWheel.GetGroundHit(out hit);

            travelL = (-wheelColliders.FLWheel.transform.InverseTransformPoint(hit.point).y - wheelColliders.FLWheel.radius) / wheelColliders.FLWheel.suspensionDistance;
            travelR = (-wheelColliders.FRWheel.transform.InverseTransformPoint(hit.point).y - wheelColliders.FRWheel.radius) / wheelColliders.FRWheel.suspensionDistance;

            float antiRollForce = (travelL - travelR) * 5000.0f;

            chassisRB.AddForceAtPosition(wheelColliders.FLWheel.transform.up * -antiRollForce, new Vector3(wheelColliders.FLWheel.transform.localPosition.x, 0.3f, 0));
            chassisRB.AddForceAtPosition(wheelColliders.FRWheel.transform.up * -antiRollForce, new Vector3(wheelColliders.FRWheel.transform.localPosition.x, 0.3f, 0));
        }
    }

    private bool isGrounded()
    {
        return (wheelColliders.FRWheel.isGrounded && wheelColliders.FLWheel.isGrounded
            && wheelColliders.RRWheel.isGrounded && wheelColliders.RLWheel.isGrounded);
    }

    void AnimateWheels()
    {
        UpdateWheel(wheelColliders.FRWheel, wheelMeshes.FRWheel);
        UpdateWheel(wheelColliders.FLWheel, wheelMeshes.FLWheel);
        UpdateWheel(wheelColliders.RRWheel, wheelMeshes.RRWheel);
        UpdateWheel(wheelColliders.RLWheel, wheelMeshes.RLWheel);
    }

    void UpdateWheel(WheelCollider coll, Transform mesh)
    {
        Quaternion quat;
        Vector3 position;

        coll.GetWorldPose(out position, out quat);

        mesh.transform.position = position;
        mesh.transform.rotation = quat;
    }

    void DrawPath()
    {
        lineRenderer.positionCount = navMeshAgent.path.corners.Length;
        lineRenderer.SetPosition(0, transform.position);

        if (navMeshAgent.path.corners.Length < 2)
            return;

        for (int i = 1; i < navMeshAgent.path.corners.Length; i++)
        {
            Vector3 pointPos = new Vector3(navMeshAgent.path.corners[i].x, navMeshAgent.path.corners[i].y, navMeshAgent.path.corners[i].z);
            lineRenderer.SetPosition(i, pointPos);
        }
    }

    public enum TransmissionMode
    {
        Parking,
        Reverse,
        Neutral,
        Drive,
        Gear3,
        Gear2
    }

    [System.Serializable]
    public class WheelColliders
    {
        public WheelCollider FRWheel;
        public WheelCollider FLWheel;
        public WheelCollider RRWheel;
        public WheelCollider RLWheel;
    }

    [System.Serializable]
    public class WheelMeshes
    {
        public Transform FRWheel;
        public Transform FLWheel;
        public Transform RRWheel;
        public Transform RLWheel;
    }
}
