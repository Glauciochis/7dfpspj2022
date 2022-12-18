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
    [SerializeField] private GameObject contextUI;
    [SerializeField] private TMPro.TMP_Text contexttext;
    private GameObject hoverObject;

    private float pitch = 0;
    private float yaw = 0;
    private float roll = 0;
    private float headheight = 0;

    // private movement things
    private float currentjump = 0;
    private float jumpheight = .4f;
    private float jumpboost = 7f;

    [HideInInspector] public Vector3 Velocity;
    [HideInInspector] public Vector3 HeadLean;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isRunning;
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

    public List<Firearm> Firearms;
    public int SelectedFirearm = 0;

    void Start()
    {
        WorldBase.player = this;
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
        isMoving = false;
        if (CanControl && CanMove && m.magnitude > 0)
        {
            isMoving = true;
            Velocity += ((hv * m.x) + (vv * m.y)) * Time.deltaTime * 12;
        }

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

        // INTERACTION
        contextUI.SetActive(false);
        hoverObject = null;
        if (Physics.Raycast(new Ray(cam.transform.position, cam.transform.forward), out RaycastHit ihit, 2))
        {
            if (ihit.transform.gameObject.TryGetComponent(out InteractionObject io))
            { contexttext.text = io.Context; contextUI.SetActive(true); }

            hoverObject = ihit.transform.gameObject;
        }
    }
    void FixedUpdate()
    {
        // PHYSICS
        // get det ray
        var rayDown = new Ray(transform.position, Vector3.down);
        // move with velocity
        Velocity.y -= .2f + (isJumping ? 0 : .3f);
        if (isGrounded && Velocity.y < 0 && !isJumping) { Velocity.y = 0; }
        charcon.Move(Velocity * .01f);
        Velocity.x *= .9f;
        Velocity.z *= .9f;

        // SLOPE CORRECTION
        if (isGrounded && isMoving && !isJumping && Physics.Raycast(rayDown, out RaycastHit ghit, LayerMask.GetMask("Ground")))
        {
            var rayDown2 = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(rayDown2, out RaycastHit ghit2, LayerMask.GetMask("Ground")))
            {
                if (Mathf.Abs(ghit2.point.y - ghit.point.y) <= .25f)
                { charcon.Move(new Vector3(0, ghit2.point.y - ghit.point.y, 0)); }
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
    void OnFire(InputValue input)
    {
        if (input.isPressed) { Firearms[SelectedFirearm].PressTrigger(); }
        else { Firearms[SelectedFirearm].ReleaseTrigger(); }
    }
    void OnReload(InputValue input)
    {
        if (input.isPressed) { Firearms[SelectedFirearm].Reload(); }
    }
    void OnScroll(InputValue input)
    {
        Firearms[SelectedFirearm].gameObject.SetActive(false);

        SelectedFirearm += Mathf.Min(Mathf.Max((int)input.Get<float>(), -1), 1);
        if (SelectedFirearm > Firearms.Count - 1) { SelectedFirearm = 0; }
        else if (SelectedFirearm < 0) { SelectedFirearm = Firearms.Count - 1; }

        Firearms[SelectedFirearm].gameObject.SetActive(true);
    }
    void OnInteract(InputValue input)
    {
        if (hoverObject && input.isPressed) { hoverObject.SendMessage("OnInteract", this, SendMessageOptions.DontRequireReceiver); }
    }
}
