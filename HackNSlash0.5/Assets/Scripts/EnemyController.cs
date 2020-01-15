using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour{
    public CharacterController cc; // Character Controller Component
    public enum attackType {SideSlash, DownSlash, SpinSlash};
    public Animator animator;
    private bool isAttacking = false;
    public bool invincible = false;
    private float iFrames = 1;
    // Start is called before the first frame update
    void Start() {
        cc = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        if (!cc.isGrounded) { // Gravity
            cc.Move(Vector3.up * -1);
        }

        // Manages isAttacking  // HOLY CRAP THIS IS UGLY
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Enter")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Enter 0")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Enter 1")) { isAttacking = true; }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Exit")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Exit 0")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Exit 1")) { isAttacking = false; }
    }

    /// <summary> Moves the Enemy a distance of moveVector units. </summary>
    /// <param name="moveVector"> Distance the Enemy should move. </param>
    public void move(Vector2 moveVector) {
        Vector3 moveVector3 = new Vector3(moveVector.x, 0f, moveVector.y);
        StartCoroutine(moveCoroutine(moveVector3, 1));
    }

    /// <summary> Moves the Enemy a distance of moveVector units. </summary>
    /// <param name="moveVector"> Distance the Enemy should move. </param>
    public void move(Vector3 moveVector) {
        StartCoroutine(moveCoroutine(moveVector, 1));
    }

    /// <summary> Moves the Enemy a distance of moveVector units at a specified speed. </summary>
    /// <param name="moveVector"> Distance the Enemy should move. </param>
    /// <param name="speed"> Movement Speed of the Enemy. </param>
    public void move(Vector3 moveVector, float speed) {
        StartCoroutine(moveCoroutine(moveVector, speed));
    }

    /// <summary> Plays a Random Attack Animation. </summary>
    public void attack() {
        int attack = Random.Range(0,3);
        switch (attack) {
            case 0:
                animator.SetTrigger("SideSlash");
                break;
            case 1:
                animator.SetTrigger("DownSlash");
                break;
            case 2:
                animator.SetTrigger("SpinSlash");
                break;
            default:
                animator.SetTrigger("SideSlash");
                Debug.Log("ERROR");
                break;
        }
    }

    /// <summary> Plays a specified Attack Animation. </summary>
    /// <param name="attackType"> Which Attack to be used. </param>
    public void attack(attackType attackType) {
        int attack = (int) attackType;
        switch (attack) {
            case 0:
                animator.SetTrigger("SideSlash");
                break;
            case 1:
                animator.SetTrigger("DownSlash");
                break;
            case 2:
                animator.SetTrigger("SpinSlash");
                break;
            default:
                animator.SetTrigger("SideSlash");
                Debug.Log("ERROR");
                break;
        }
    }

    /// <summary> Rotates the Enemy a set Rotation. </summary>
    /// <param name="rotation"> Amount the enemy should be rotated. </param>
    public void rotate(float rotation) {
        StartCoroutine(rotateCoroutine(rotation, 1f));
    }

    /// <summary> Rotates the Enemy a set Rotation at a specified speed. </summary>
    /// <param name="rotation"> Amount the enemy should be rotated. </param>
    /// <param name="speed"> Rotation Speed of the Enemy. </param>
    public void rotate(float rotation, float speed) {
        StartCoroutine(rotateCoroutine(rotation, speed));
    }

    /// <summary> Moves the Enemy a distance of moveVector units at a specified speed. </summary>
    /// <param name="moveVector"> Distance the Enemy should move. </param>
    /// <param name="speed"> Movement Speed of the Enemy. </param>
    public IEnumerator moveCoroutine(Vector3 moveVector, float speed) {
        Vector3 totalVector = Vector3.zero;
        Vector3 localVector = (transform.forward * moveVector.normalized.z) + (transform.right * moveVector.normalized.x);

        animator.SetFloat("SpeedX", moveVector.normalized.x);
        animator.SetFloat("SpeedY", moveVector.normalized.z);

        while (totalVector.magnitude < moveVector.magnitude) {
            localVector = (transform.forward * moveVector.normalized.z) + (transform.right * moveVector.normalized.x);
            totalVector += (moveVector.normalized * speed);
            cc.Move(localVector * speed);
            Debug.Log("100");
            yield return 0;
        }
        animator.SetFloat("SpeedX", 0);
        animator.SetFloat("SpeedY", 0);
    }

    /// <summary> Rotates the Enemy a set Rotation at a specified speed. </summary>
    /// <param name="rotation"> Amount the enemy should be rotated. </param>
    /// <param name="speed"> Rotation Speed of the Enemy. </param>
    public IEnumerator rotateCoroutine(float rotation, float speed) {
        float totalRotation = 0;

        while (totalRotation < rotation) {
            totalRotation += speed;
            gameObject.transform.Rotate(new Vector3(0f, speed, 0f));
            yield return 0;
        }
    }

    public void takeDamage(int damage)
    {
        if (!invincible)
        {
            StartCoroutine(wasHit());
            print("ouch");
        }
    }

    IEnumerator wasHit()
    {
        invincible = true;
        yield return new WaitForSeconds(iFrames);
        invincible = false;
    }
}
