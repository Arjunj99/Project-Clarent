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
    
    [Header("Acceleration Curves")]
    public AnimationCurve forwardAnimation;
    





    // Start is called before the first frame update
    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
