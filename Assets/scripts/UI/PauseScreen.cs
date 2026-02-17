using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject PauseScreenObj;
    private new_inputSystem inputs;

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
        inputs.Player.Previous.performed += _ => Pause();
    }

    public void Pause()
    {
        PauseScreenObj.SetActive(!PauseScreenObj.activeSelf);
        Time.timeScale = PauseScreenObj.activeSelf ? 0 : 1;
        GameEventHandler.Instance.OnGamePaused?.Invoke();
    }
}
