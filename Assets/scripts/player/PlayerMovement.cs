using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float EnginePower = 10f;
    [SerializeField] private float shipTorqe = 20f;
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
        if (inputs.Player.Move.IsInProgress())
        {
            Move(inputs.Player.Move.ReadValue<Vector2>());
        }
        Rotate();

    }

    private void Move(Vector2 v)
    {
        _r.AddForce(v.normalized * EnginePower, ForceMode2D.Force);
    }

    private void Rotate()
    {
        _t.rotation = Quaternion.RotateTowards(_t.rotation, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, Camera.main.ScreenToWorldPoint(Mouse.current.position.value) - _t.position)), shipTorqe * Time.timeScale);
    }
}
