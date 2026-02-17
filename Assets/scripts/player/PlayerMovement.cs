using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float EnginePower = 10f;
    [SerializeField] private float shipTorque = 20f;
    [SerializeField] private ParticleSystem flames;

    private Rigidbody2D _r;
    private Transform _t;
    private new_inputSystem inputs;

    public static GameObject PlayerInstance { get; private set; }

    private void Awake()
    {
        inputs = new new_inputSystem();
        PlayerInstance = gameObject;
    }

    void OnDisable()
    {
        inputs.Disable();
    }

    void OnEnable()
    {
        inputs.Enable();
    }

    void Start()
    {
        _r = GetComponent<Rigidbody2D>();
        _t = transform;

        inputs.Player.Move.performed += _ => flames.Play();
        inputs.Player.Move.canceled += _ => flames.Stop();
    }

    void Update()
    {
        if (SaveEngine.Instance.Data.settings.Platform == Platform.PC)
        {
            PCMovement();
            return;
        }

        AndroidMovement();
    }

    private void AndroidMovement()
    {
        MoveUsingJoyStick();

        if (inputs.Player.Heading.IsInProgress())
            Rotate((Vector2)_t.position + inputs.Player.Heading.ReadValue<Vector2>());
    }

    private void PCMovement()
    {
        MoveUsingJoyStick();

        Rotate(Camera.main.ScreenToWorldPoint(Mouse.current.position.value));
    }

    private void MoveUsingJoyStick()
    {
        if (inputs.Player.Move.IsInProgress())
            Move(inputs.Player.Move.ReadValue<Vector2>());
    }

    private void Move(Vector2 v)
    {
        _r.AddForce(v.normalized * EnginePower, ForceMode2D.Force);
    }

    private void Rotate(Vector2 heading)
    {
        _t.rotation = Quaternion.RotateTowards(_t.rotation, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, (heading - (Vector2)_t.position).normalized)), shipTorque * Time.timeScale);
    }
}
