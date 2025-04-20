using UnityEngine;
using UnityEngine.InputSystem;

public class PlayInput : MonoBehaviour
{
    public static PlayInput instance;

    public Vector2 Move {  get; private set; }
    public bool Dash {  get; private set; }

    public bool Jump { get; private set; }

    public bool JumpHeld { get; private set; }

    public bool Hover { get; private set; }

    public bool HoverHeld { get; private set; }

    public bool HoverUp { get; private set; }

    public bool Menu { get; private set; }

    private PlayerInput playerInput;

    private InputAction moveAction;
    private InputAction dashAction;
    private InputAction jumpAction;
    private InputAction hoverAction;
    private InputAction menuAction;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        playerInput = GetComponent<PlayerInput>();

        SetupInputActions();
    }
    private void Update()
    {
        UpdateInput();
    }
    private void SetupInputActions()
    {
        moveAction = playerInput.actions["Move"];
        dashAction = playerInput.actions["Dash"];
        jumpAction = playerInput.actions["Jump"];
        hoverAction = playerInput.actions["Hover"];
        menuAction = playerInput.actions["Menu"];
    }
    private void UpdateInput()
    {
        Move = moveAction.ReadValue<Vector2>();
        Dash = dashAction.WasPressedThisFrame();
        Jump = jumpAction.WasPressedThisFrame();
        JumpHeld = jumpAction.IsPressed();
        Hover = hoverAction.WasPressedThisFrame();
        HoverHeld = hoverAction.IsPressed();
        HoverUp = hoverAction.WasReleasedThisFrame();
        Menu = menuAction.WasPressedThisFrame();
    }
}
