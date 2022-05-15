using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public AnimationCurve accelCurve;
    public AnimationCurve deaccelCurve;

    AnimationCurve currentCurve;
    float accelTime = 6f / 60f;
    float deaccelTime = 3f / 60f;
    float curveTiming;
    float topSpeed = 2f;
    float currentSpeed = 0f;
    float timeSinceAccelStart = 0f;
    bool inDash = false;
    bool inRoll = false;
    Vector3 accelDir;
    internal Vector3 velocity;


    SpriteRenderer renderer;
    private static readonly int SpeedX = Animator.StringToHash("speed_x");
    private static readonly int SpeedY = Animator.StringToHash("speed_y");
    private static readonly int Dash = Animator.StringToHash("dash");
    private static readonly int Roll = Animator.StringToHash("roll");

    // Start is called before the first frame update
    void Start()
    {
        renderer= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){

        Vector3 velocity=new Vector3(0,0,0);
        Vector3 keyInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        Vector3 normalizedDir = keyInput.normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            animator.SetBool(Dash, true);
            inDash = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl)) {
            animator.SetBool(Roll, true);
            inRoll = true;
        }
        
        

        if (currentSpeed > 0) {
            if (!currentCurve.Equals(deaccelCurve) && currentSpeed > 0.99 * topSpeed && normalizedDir.x == 0 &&
                normalizedDir.y == 0) {
                currentCurve = deaccelCurve;
                curveTiming = deaccelTime;
                timeSinceAccelStart = 0;
            }

            if (normalizedDir.magnitude > 0 && normalizedDir != accelDir)
                accelDir = normalizedDir;
            timeSinceAccelStart += Time.deltaTime;
            velocity = accelDir * (topSpeed * currentCurve.Evaluate(timeSinceAccelStart / accelTime));
            //Debug.Log(velocity);
        }
        else {
            currentCurve = accelCurve;
            curveTiming = accelTime;
            velocity = normalizedDir * (topSpeed * accelCurve.Evaluate(Time.deltaTime / accelTime));
            accelDir = normalizedDir;
        }


        if (inDash)
            velocity = accelDir * (1.5f * topSpeed);
        else if (inRoll) {
            velocity = accelDir * (1.25f * topSpeed);
        }

        var move = velocity * Time.deltaTime;
        this.velocity = velocity;

        currentSpeed = Mathf.Abs(velocity.x) + Mathf.Abs(velocity.y);
        



    }

    void FixedUpdate()
    {
        Vector3 move= velocity * Time.deltaTime;
        if (currentSpeed > 0) {
            transform.Translate(move);
            animator.SetFloat(SpeedX, Mathf.Abs(move.x));
            animator.SetFloat(SpeedY, move.y);
        }

        if (move.x > 0.001)
            renderer.flipX = false;
        else if (move.x < -0.001)
            renderer.flipX = true;

    }

    public void OnDashEnded() {
        animator.SetBool(Dash, false);
        inDash = false;
        currentCurve = accelCurve;

    }
    public void OnRollEnded()
    {
        animator.SetBool(Roll, false);
        inRoll = false;
        currentCurve = accelCurve;


    }


}
