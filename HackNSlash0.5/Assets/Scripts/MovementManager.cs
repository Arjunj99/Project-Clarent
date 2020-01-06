﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A GameObject with this script has its movement controlable by the player.
/// </summary>
/// <author> Arjun Jaishankar </author>
/// <version> 11/21/2019 </version>
public class MovementManager : MonoBehaviour {
    [SerializeField] private CharacterController characterController;
    [Header("Animation Settings")]
    public Animator animator;

    [Header("Speed Settings")]
    [SerializeField] private float forwardMax, backwardMax, rightMax, leftMax;
    private Vector3 velocity = Vector3.zero;

    [Header("Acceleration Curves")]
    [SerializeField] private AnimationCurve forwardCurve, backwardCurve, rightCurve, leftCurve;
    public float forwardT, backwardT, rightT, leftT;
    [SerializeField] private float forwardTimePeriod, backwardTimePeriod, rightTimePeriod, leftTimePeriod; 

    [Header("Key Bindings")]
    public KeyCode forward, backward, right, left;

    [Header("Camera Settings")]
    public CameraManager mainCamera;

    // Start is called before the first frame update
    void Start() {
        forwardT = 0; backwardT = 0; rightT = 0; leftT = 0;
    }

    // Update is called once per frame
    void Update() {
        manageTime();
        manageAnimations();
        applyVelocity();
        rotatePlayer();

    }

    void lateUpdate() {
        // rotatePlayer();
    }

    /// <summary>
    /// ManageTime sets the time spent in each of the four movement states.
    /// </summary>
    private void manageTime() {
        forwardT += adjustVelocity(forward, forwardT, forwardTimePeriod, 3);
        backwardT += adjustVelocity(backward, backwardT, backwardTimePeriod, 3);
        rightT += adjustVelocity(right, rightT, rightTimePeriod, 3);
        leftT += adjustVelocity(left, leftT, leftTimePeriod, 3);
    }

    /// <summary>
    /// AdjustTime generates a float that denotes delta time for a specific movement state.
    /// </summary>
    /// <param name="button">
    /// KeyCode used for a specific movement state.
    /// </param>
    /// <param name="time">
    /// Current time spent in a specific movement state.
    /// </param>
    /// <param name="timePeriod">
    /// Total time needed to reach max acceleration.
    /// </param>
    /// <param name="speedDecay">
    /// Multiplier for Acceleration Decay when not pressing button.
    /// </param>
    /// <returns>
    /// Float that denotes delta time for a specific movement state.
    /// </returns>
    private float adjustVelocity(KeyCode button, float time, float timePeriod, float speedDecay) {
        if (Input.GetKey(button) && time < 1) { return (Time.deltaTime/timePeriod); } 
        if (!Input.GetKey(button) && time > 0) { return (-Time.deltaTime/timePeriod) * speedDecay; }
        else if (!Input.GetKey(button) && time < 0) { return -time; }
        else { return 0; }
    }
    
    /// <summary>
    /// ApplyVelocity applies all four directional velocities to the player using the CharacterController component.
    /// </summary>
    private void applyVelocity() {
        velocity = Vector3.zero;
        velocity += gameObject.transform.forward * forwardCurve.Evaluate(forwardT);
        velocity -= gameObject.transform.forward * forwardCurve.Evaluate(backwardT);
        velocity += gameObject.transform.right * forwardCurve.Evaluate(rightT);
        velocity -= gameObject.transform.right * forwardCurve.Evaluate(leftT);

        characterController.Move(velocity);
    }

    private void manageAnimations() {
        if (forwardT > 0f || backwardT > 0f) {
            // Debug.Log("Running");
            animator.SetBool("isRunning", true);
            animator.SetBool("isStrafing", false);
        } else if (rightT > 0f || leftT > 0f) {
            // Debug.Log("Strafing");
            animator.SetBool("isRunning", false);
            animator.SetBool("isStrafing", true);
        } else {
            // Debug.Log("Idle");
            animator.SetBool("isRunning", false);
            animator.SetBool("isStrafing", false);
        }
    }

    private void rotatePlayer() {
        // Vector3 currentRotation = QuaternionToVector3(gameObject.transform.rotation);
        // float cameraRotation = mainCamera.transform.rotation.y * Mathf.Rad2Deg;
        // Debug.Log(cameraRotation);

        // gameObject.transform.rotation = Quaternion.Euler(applyCameraRotation(gameObject, mainCamera.cameraRotation.y));

        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(applyCameraRotation(gameObject, mainCamera.cameraRotation.y)), 0.6f);
    }

    private static Vector3 QuaternionToVector3(Quaternion quat) {
        return new Vector3 (quat.x, quat.y, quat.z);
    }

    private static Vector3 applyCameraRotation(GameObject gameObject, float cameraRotation) {
        return new Vector3 (gameObject.transform.rotation.x, cameraRotation, gameObject.transform.rotation.z);
    }
}
