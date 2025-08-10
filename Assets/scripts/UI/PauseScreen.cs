using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject PauseScreenObj;
    private new_inputSystem inputs;
    private bool _paused = false;
    private void Awake()
    {
        inputs = new new_inputSystem();
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
        inputs.Player.Previous.performed += pause;
    }

    private void pause(InputAction.CallbackContext context)
    {
        _paused = !_paused;
        Time.timeScale = _paused ? 0 : 1;
        PauseScreenObj.SetActive(_paused);
        GameEventHandler.OnGamePuased?.Invoke();
    }
}
