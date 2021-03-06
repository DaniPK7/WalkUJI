/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class setpCounter : MonoBehaviour
{
    StepCounter stepCounter;
    public TextMeshProUGUI stepText;

    public int currentSteps = 0;

    // Start is called before the first frame update
    void Start()
    {
        InputSystem.EnableDevice(StepCounter.current);
        StepCounter.current.samplingFrequency = 16;
    }

    // Update is called once per frame
    void Update()
    {
        stepCounter = StepCounter.current;
        
        if (stepCounter.enabled)
        {
            Debug.Log("StepCounter is enabled");

            currentSteps = stepCounter.stepCounter.ReadValue();

            Debug.Log("StepCounter count: "+ StepCounter.current.stepCounter.ReadValue());

            stepText.text = "Steps: " + currentSteps;

        }


    }
}

*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class setpCounter : MonoBehaviour
{

	public TextMeshProUGUI acc;

	//public MoverMaster mover;

	public float loLim = 0.005f; // level to fall to the low state 
	public float hiLim = 0.25f; // level to go to high state (and detect step) 
	public int steps = 0; // step counter - counts when comp state goes high private 
	bool stateH = false; // comparator state

	public float fHigh = 10.0f; // noise filter control - reduces frequencies above fHigh private 
	public float curAcc = 0f; // noise filter 
	public float fLow = 0.1f; // average gravity filter control - time constant about 1/fLow 
	float avgAcc = 0f;

	public int wait_time = 30;
	private int old_steps;
	private int counter = 30;

	void Awake()
	{
		avgAcc = Input.acceleration.magnitude; // initialize avg filter
		old_steps = steps;
	}

	void Update()
	{
		if (counter > 0)
		{
			counter--;
			return;
		}
		counter = wait_time;
		/*if (steps != old_steps)
			mover.walk = true;
		else
			mover.walk = false;
		old_steps = steps;*/
	}

	void FixedUpdate()
	{ // filter input.acceleration using Lerp
		curAcc = Mathf.Lerp(curAcc, Input.acceleration.magnitude, Time.deltaTime * fHigh);
		avgAcc = Mathf.Lerp(avgAcc, Input.acceleration.magnitude, Time.deltaTime * fLow);
		float delta = curAcc - avgAcc; // gets the acceleration pulses
		if (!stateH)
		{ // if state == low...
			if (delta > hiLim)
			{ // only goes high if input > hiLim
				stateH = true;
				steps++; // count step when comp goes high 
				acc.text = "steps:" + steps;
			}
		}
		else
		{
			if (delta < loLim)
			{ // only goes low if input < loLim 
				stateH = false;
			}
		}
	}

}