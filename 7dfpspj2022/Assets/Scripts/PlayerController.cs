using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController charcon;
    private Camera cam;
    [SerializeField] private Transform head;
    [SerializeField] private Transform groundsphere;

    private float pitch = 0;
    private float yaw = 0;
    private float roll = 0;
    private float headheight = 0;

    // private movement things
    private float currentjump = 0;
    private float jumpheight = .4f;
    private float jumpboost = 2f;

    [HideInInspector] public Vector3 Velocity;
    [HideInInspector] public Vector3 HeadLean;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isRunning;
    [HideInInspector] public bool isWalking;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isCrouching;
    [HideInInspector] public bool wasGrounded;

    // restrictions/cans
    public bool CanControl = true;
    public bool CanLook = true;
    public bool CanMove = true;
    // speeds
    public float WalkSpeed = 3f;
    public float RunSpeed = 5f;

    [HideInInspector] public Vector2 Movement;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        charcon = GetComponent<CharacterController>();
        cam = Camera.main;
        headheight = head.transform.localPosition.y;
    }
    void Update()
    {
        // GROUND CHECK
        isGrounded = Physics.CheckSphere(groundsphere.position, 0.225f, ~LayerMask.GetMask("Player"));
        if (wasGrounded != isGrounded && isGrounded) { OnLand(); }
        wasGrounded = isGrounded;

        //  MOVEMENT
        // determine desired speed
        float s = WalkSpeed;
        if (isRunning) { s = RunSpeed; }
        Vector3 m = Movement * s;
        // calculate directional movement
        Vector3 hv = new Vector3(Mathf.Cos(Mathf.Deg2Rad * -yaw), 0, Mathf.Sin(Mathf.Deg2Rad * -yaw));
        Vector3 vv = new Vector3(Mathf.Cos((Mathf.Deg2Rad * -yaw) + Mathf.PI / 2), 0, Mathf.Sin((Mathf.Deg2Rad * -yaw) + Mathf.PI / 2));
        // move
        if (CanControl && CanMove)
        { Velocity += ((hv * m.x) + (vv * m.y)) * Time.deltaTime * 12; }

        // LOOKING
        pitch = Mathf.Max(Mathf.Min(pitch, 90), -90);
        head.transform.localEulerAngles = new Vector3(pitch, yaw, roll);
        head.transform.localPosition = HeadLean + new Vector3(0, headheight, 0);

        // JUMPING
        if (isJumping)
        {
            currentjump += Time.deltaTime;
            Velocity.y = jumpboost;
            if (currentjump >= jumpheight) { isJumping = false; }
        }

        // PHYSICS
        // get det ray
        var rayDown = new Ray(transform.position, Vector3.down);
        // move with velocity
        Velocity.y -= (4f * Time.deltaTime) + ((isJumping ? 0 : 8f) * Time.deltaTime);
        if (isGrounded && Velocity.y < 0 && !isJumping) { Velocity.y = 0; }
        charcon.Move(Velocity * Time.deltaTime);
        Velocity.x *= .95f;
        Velocity.z *= .95f;

        // SLOPE CORRECTION
        if (isGrounded && Physics.Raycast(rayDown, out RaycastHit ghit, LayerMask.GetMask("Ground")))
        {
            var rayDown2 = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(rayDown2, out RaycastHit ghit2, LayerMask.GetMask("Ground")))
            {
                charcon.Move(new Vector3(0, ghit2.point.y - ghit.point.y, 0));
            }
        }
    }

    void OnLand()
    {
        //Debug.Log("landed");
    }

    void OnMove(InputValue input) { Movement = input.Get<Vector2>(); }
    void OnLook(InputValue input)
    {
        if (CanControl && CanLook)
        {
            var look = input.Get<Vector2>();
            if (PlayerPrefs.GetInt("invert mouse y", 1) == 1) { look.y = -look.y; }
            pitch += look.y * 0.2f;
            yaw += look.x * 0.2f;
        }
    }
    void OnJump(InputValue input)
    {
        isJumping = isGrounded ? (input.isPressed && CanControl) : false;
        if (isJumping) { Velocity.y = jumpboost; currentjump = 0; }
    }
}
