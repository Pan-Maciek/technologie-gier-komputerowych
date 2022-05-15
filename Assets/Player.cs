using UnityEngine;

public class Player : MonoBehaviour {
    public Animator animator;
    public AnimationCurve accelCurve;
    public AnimationCurve deaccelCurve;

    AnimationCurve currentCurve;
    float accelTime = 6f / 60f;
    float deaccelTime = 3f / 60f;
    float curveTiming;
    float topSpeed = 5f;
    float currentSpeed;
    float timeSinceAccelStart;
    bool inDash;
    bool inRoll;
    Vector3 accelDir;
    internal Vector3 velocity;


    SpriteRenderer renderer;
    private static readonly int SpeedX = Animator.StringToHash("speed_x");
    private static readonly int SpeedY = Animator.StringToHash("speed_y");
    private static readonly int Dash = Animator.StringToHash("dash");
    private static readonly int Roll = Animator.StringToHash("roll");

    // Start is called before the first frame update
    void Start() {
        var position = transform.position;
        var key = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));

        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        var velocity = new Vector3(0, 0, 0);
        var keyInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        var normalizedDir = keyInput.normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            animator.SetBool("dash", true);
            inDash = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl)) {
            animator.SetBool("roll", true);
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
        if (currentSpeed > 0) {
            transform.Translate(move);
            animator.SetFloat(SpeedX, Mathf.Abs(move.x));
            animator.SetFloat(SpeedY, move.y);
        }

        if (move.x > 0.01)
            renderer.flipX = false;
        else if (move.x < -0.01)
            renderer.flipX = true;
    }

    public void OnDashEnded() {
        animator.SetBool(Dash, false);
        inDash = false;
        currentCurve = accelCurve;
    }

    public void OnRollEnded() {
        animator.SetBool(Roll, false);
        inRoll = false;
        currentCurve = accelCurve;
    }
}