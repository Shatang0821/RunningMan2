using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 入力処理
/// </summary>
public class PlayerInput
{
    #region 変数定義

    private InputActions _inputActions;
    private InputDevice _currentDevice; //現在デバイス

    public Vector2 Axis => ProcessInput(_inputActions.GamePlay.Axis.ReadValue<Vector2>());

    //ジャンプキーが押されたとき
    public bool Jump => _inputActions.GamePlay.Jump.WasPerformedThisFrame();

    //ジャンプが離れたとき
    public bool StopJump => _inputActions.GamePlay.Jump.WasReleasedThisFrame();

    //ダッシュキーが押されたとき
    public bool Dash => _inputActions.GamePlay.Dash.WasPerformedThisFrame();

    //登るキーが押し続けている間
    public bool Climb => _inputActions.GamePlay.Climb.IsPressed();

    #endregion

    #region クラスライフサイクル

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Init()
    {
        _inputActions = new InputActions();
        _currentDevice = Keyboard.current;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        InputSystem.onActionChange += OnActionChange;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        InputSystem.onActionChange -= OnActionChange;
    }

    #endregion

    #region デバイス

    /// <summary>
    /// デバイス切り替え処理
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="actionChange"></param>
    private void OnActionChange(object obj, InputActionChange actionChange)
    {
        if (actionChange == InputActionChange.ActionStarted)
        {
            var d = ((InputAction)obj).activeControl.device;
            switch (d.device)
            {
                case Keyboard:
                    if (_currentDevice == Keyboard.current)
                        return;
                    _currentDevice = Keyboard.current;
                    Debug.Log(_currentDevice);
                    break;
                case Gamepad:
                    if (_currentDevice == Gamepad.current)
                        return;
                    _currentDevice = Gamepad.current;
                    Debug.Log(_currentDevice);
                    break;
            }
        }
    }

    #endregion

    #region 移動入力

    /// <summary>
    /// デバイス判断して方向ベクトルを返す
    /// </summary>
    /// <param name="rawInput"></param>
    /// <returns></returns>
    Vector2 ProcessInput(Vector2 rawInput)
    {
        // activeControlがnullでないか、そのデバイスがゲームパッドかをチェック
        if (_currentDevice == Gamepad.current)
        {
            // ゲームパッドの入力を方向入力に変換
            return DirectionInput(rawInput.x, rawInput.y);
        }
        else
        {
            // キーボードなどの他の入力デバイスからの入力は無視
            return rawInput;
        }
    }

    /// <summary>
    /// パッドの角度からベクトル計算
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    Vector2 DirectionInput(float x, float y)
    {
        // 入力がゼロかどうかをチェック
        if (x == 0 && y == 0)
        {
            return Vector2.zero;
        }

        // 角度を計算
        var angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        // 角度が負の場合、360度の範囲に変換
        if (angle < 0)
        {
            angle += 360;
        }

        //方向を計算する
        int segment = Mathf.FloorToInt((angle + 22.5f) / 45) % 8;

        switch (segment)
        {
            case 0: return Vector2.right; // East
            case 1: return new Vector2(1, 1); // Northeast
            case 2: return Vector2.up; // North
            case 3: return new Vector2(-1, 1); // Northwest
            case 4: return Vector2.left; // West
            case 5: return new Vector2(-1, -1); // Southwest
            case 6: return Vector2.down; // South
            case 7: return new Vector2(1, -1); // Southeast
            default:
                return Vector2.zero;
                //throw new InvalidOperationException("Unexpected angle calculation.");
                break;
        }
    }

    #endregion
}