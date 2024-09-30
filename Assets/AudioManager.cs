using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip hillStart;
    [SerializeField] AudioClip hillEnd;

    [SerializeField] AudioClip parallelStart;
    [SerializeField] AudioClip parallelEnd;

    [SerializeField] AudioClip rampStart;
    [SerializeField] AudioClip rampEnd;

    [SerializeField] AudioClip threeCornersStart;
    [SerializeField] AudioClip threeCornersEnd;

    [SerializeField] AudioClip sCurveStart;
    [SerializeField] AudioClip sCurveEnd;

    [SerializeField] AudioClip zCurveStart;
    [SerializeField] AudioClip zCurveEnd;

    [SerializeField] AudioSource speakerFL;
    [SerializeField] AudioSource speakerFR;
    [SerializeField] AudioSource speakerRL;
    [SerializeField] AudioSource speakerRR;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HillStart()
    {
        speakerFL.PlayOneShot(hillStart);
    }

    public void HillEnd()
    {
        speakerFL.PlayOneShot(hillEnd);
    }

    public void ParallelStart()
    {
        speakerFL.PlayOneShot(parallelStart);
    }

    public void ParallelEnd()
    {
        speakerFL.PlayOneShot(parallelEnd);
    }

    public void RampStart()
    {
        speakerFL.PlayOneShot(rampStart);
    }

    public void RampEnd()
    {
        speakerFL.PlayOneShot(rampEnd);
    }

    public void ThreeCornersStart()
    {
        speakerFL.PlayOneShot(threeCornersStart);
    }

    public void ThreeCornersEnd()
    {
        speakerFL.PlayOneShot(threeCornersEnd);
    }

    public void SCurveStart()
    {
        speakerFL.PlayOneShot(sCurveStart);
    }

    public void SCurveEnd()
    {
        speakerFL.PlayOneShot(sCurveEnd);
    }

    public void ZCurveStart()
    {
        speakerFL.PlayOneShot(zCurveStart);
    }

    public void ZCurveEnd()
    {
        speakerFL.PlayOneShot(zCurveEnd);
    }

    public void PlaySpeaker(AudioClip clip)
    {

        speakerFL.Stop();
        speakerFR.Stop();
        speakerRL.Stop();
        speakerRR.Stop();

        speakerFL.PlayOneShot(clip);
        speakerFR.PlayOneShot(clip);
        speakerRL.PlayOneShot(clip);
        speakerRR.PlayOneShot(clip);
    }
}
