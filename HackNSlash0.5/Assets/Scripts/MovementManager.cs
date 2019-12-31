using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A GameObject with this script has its movement controlable by the player.
/// </summary>
/// <author> Arjun Jaishankar </author>
/// <version> 11/21/2019 </version>
public class MovementManager : MonoBehaviour {
    public CharacterController characterController;


    [Header("Speed Settings")]
    [SerializeField] private float forwardMax, backwardMax, rightMax, leftMax;
    public Vector3 velocity = Vector3.zero;

    [Header("Acceleration Curves")]
    [SerializeField] private AnimationCurve forwardCurve, backwardCurve, rightCurve, leftCurve;
    public float forwardT, backwardT, rightT, leftT;
    [SerializeField] private float forwardTimePeriod, backwardTimePeriod, rightTimePeriod, leftTimePeriod; 

    [Header("Key Bindings")]
    public KeyCode forward, backward, right, left;

    





    // Start is called before the first frame update
    void Start() {
        characterController = GetComponent<CharacterController>();
        forwardT = 0; backwardT = 0; rightT = 0; leftT = 0;
        // Debug.Log("D");
    }

    // Update is called once per frame
    void Update() {
        manageVelocity();
        applyVelocity();
        // Debug.Log("J");
    }

    private void manageVelocity() {
        // Debug.Log("P");

        adjustVelocity(forward, forwardT, backwardT, forwardTimePeriod, backwardTimePeriod, 3);
        adjustVelocity(backward, backwardT, forwardT, backwardTimePeriod, forwardTimePeriod, 3);
        adjustVelocity(right, rightT, leftT, rightTimePeriod, leftTimePeriod, 3);
        adjustVelocity(left, leftT, rightT, leftTimePeriod, rightTimePeriod, 3);

        // if (!Input.GetKeyDown(forward) && !Input.GetKeyDown(backward)) {
        //     if (forwardT > 0) {
        //         forwardT -= (Time.deltaTime/forwardTimePeriod) * 2;
        //     } else {
        //         forwardT = 0;
        //     }

        //     if (backwardT > 0) {
        //         backwardT -= (Time.deltaTime/backwardTimePeriod) * 2;
        //     } else {
        //         backwardT = 0;
        //     }
        // }
    }

    private void adjustVelocity(KeyCode button, float time, float reverseTime, float timePeriod, float reverseTimePeriod, int speedDecayMod) {
        if (Input.GetKey(button)) {
            Debug.Log(button.ToString());
            if (time < 1) {
                time += (Time.deltaTime/timePeriod);
            } else if (time >= 1) {
                time = 1;
            } if (reverseTime > 0) {
                reverseTime -= (Time.deltaTime/reverseTimePeriod) * speedDecayMod;
            } else if (reverseTime >= 0) {
                reverseTime = 0;
            }
        }
    }
    
    private void applyVelocity() {
        velocity += new Vector3(forwardCurve.Evaluate(forwardT) * forwardMax, 0, 0);
        velocity -= new Vector3(backwardCurve.Evaluate(backwardT) * backwardMax, 0, 0);
        velocity += new Vector3(0, 0, rightCurve.Evaluate(rightT) * rightMax);
        velocity -= new Vector3(0, 0, leftCurve.Evaluate(leftT) * leftMax);

        // Debug.Log();
        if (Input.GetKeyDown(KeyCode.P)) {
            Debug.Log(velocity);
        }
        characterController.Move(velocity);
    }


    // private void checkForInputs() {
    //     if (Input.GetKeyDown(forward)) {
    //         forwardT += (Time.deltaTime / forwardTimePeriod);
    //     } else if (Input.GetKeyDown(backward)) {
    //         forwardT += (Time.deltaTime / forwardTimePeriod);
    //     }

    //     // if (Input.GetButton(forwardButton) && forwardT < 1f) {
    //     //     forwardT += (Time.deltaTime / forwardTimePeriod);
    //     // } else if (Input.GetButton(forwardButton) && forwardT > 1f) {
    //     //     forwardT = 1f;
    //     // } else if (Input.GetButton(brakeButton) && forwardT > 0) {
    //     //     forwardT -= (Time.deltaTime * 7 / forwardTimePeriod);
    //     // } else if (Input.GetButton(brakeButton) && forwardT < 0) {
    //     //     forwardT = 0;
    //     // } else if (forwardT > 0) {
    //     //     forwardT -= (Time.deltaTime * 4 / forwardTimePeriod);
    //     // } else {
    //     //     forwardT = 0;
    //     // }
    // }
}
