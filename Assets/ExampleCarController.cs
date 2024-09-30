using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ExampleCarController : MonoBehaviour
{
    public List<string> transforms = new List<string>();

    private void transformUpdate()
    {
        if (transforms.Count == 0)
        {
            CancelInvoke();
            return;
        }

        transform.position = new Vector3(float.Parse(transforms[0]), transform.position.y, transform.position.z);
        transforms.RemoveAt(0);
        transform.position = new Vector3(transform.position.x, float.Parse(transforms[0]), transform.position.z);
        transforms.RemoveAt(0);
        transform.position = new Vector3(transform.position.x, transform.position.y, float.Parse(transforms[0]));
        transforms.RemoveAt(0);

        transform.eulerAngles = new Vector3(float.Parse(transforms[0]), transform.eulerAngles.y, transform.eulerAngles.z);
        transforms.RemoveAt(0);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, float.Parse(transforms[0]), transform.eulerAngles.z);
        transforms.RemoveAt(0);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, float.Parse(transforms[0]));
        transforms.RemoveAt(0);
    }

    public void HillClimb()
    {
        CancelInvoke();

        transforms = File.ReadAllLines(@"C:\\Users\\Name\\Documents\\NerveDrive\\Assets\\Data\\Default\\Hill Climb.txt").ToList();

        InvokeRepeating("transformUpdate", 0f, 0.03f);
    }

    public void ParallelParking()
    {
        CancelInvoke();

        transforms = File.ReadAllLines(@"C:\\Users\\Name\\Documents\\NerveDrive\\Assets\\Data\\Default\\Parallel Parking.txt").ToList();

        InvokeRepeating("transformUpdate", 0f, 0.03f);
    }

    public void ThreeCorners()
    {
        CancelInvoke();

        transforms = File.ReadAllLines(@"C:\\Users\\Name\\Documents\\NerveDrive\\Assets\\Data\\Default\\Three Corners.txt").ToList();

        InvokeRepeating("transformUpdate", 0f, 0.03f);
    }

    public void Ramp()
    {
        CancelInvoke();

        transforms = File.ReadAllLines(@"C:\\Users\\Name\\Documents\\NerveDrive\\Assets\\Data\\Default\\Ramp.txt").ToList();

        InvokeRepeating("transformUpdate", 0f, 0.03f);
    }

    public void SCurve()
    {
        CancelInvoke();

        transforms = File.ReadAllLines(@"C:\\Users\\Name\\Documents\\NerveDrive\\Assets\\Data\\Default\\S Curve.txt").ToList();

        InvokeRepeating("transformUpdate", 0f, 0.03f);
    }

    public void ZCurve()
    {
        CancelInvoke();

        transforms = File.ReadAllLines(@"C:\\Users\\Name\\Documents\\NerveDrive\\Assets\\Data\\Default\\Z Curve.txt").ToList();

        InvokeRepeating("transformUpdate", 0f, 0.03f);

    }

    // Start is called before the first frame update
    void Start()
    {
        transforms = File.ReadAllLines(@"C:\\Users\\Name\\Documents\\NerveDrive\\Assets\\Data\\Default\\All.txt").ToList();

        InvokeRepeating("transformUpdate", 2f, 0.03f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
