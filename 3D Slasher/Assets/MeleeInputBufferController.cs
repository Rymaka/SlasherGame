using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeInputBufferController : MonoBehaviour
{
    public bool _canAttack = true;
    public bool _canInput = true;
    public bool _buffering = false;
    [SerializeField] private bool _firstAttacked = true;
    [SerializeField] protected InputActionReference _inputAction;
    [SerializeField] protected InputActionReference _inputAction2;
    [SerializeField] protected MeleeComboController _firstCombo;
    [SerializeField] protected MeleeComboController _secondCombo;
    [SerializeField] private float _resetFirstAttackTimer = 0.3f;
    private Animator _anim;
    private MeleeComboController _buffer;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        _inputAction.action.performed += FirstAttack;
        _inputAction.action.performed += InputBuffer;
        _inputAction.action.performed += SimpleInput;
        _inputAction.action.Enable();
        
        _inputAction2.action.performed += FirstAttack;
        _inputAction2.action.performed += InputBuffer;
        _inputAction2.action.performed += SimpleInput;
        _inputAction2.action.Enable();
    }
    private void OnDisable()
    {
        _inputAction.action.performed -= FirstAttack;
        _inputAction.action.performed -= InputBuffer;
        _inputAction.action.performed -= SimpleInput;
        _inputAction.action.Disable();
        
        _inputAction.action.performed -= FirstAttack;
        _inputAction2.action.performed -= InputBuffer;
        _inputAction2.action.performed -= SimpleInput;
        _inputAction2.action.Disable();
    }

    private void SimpleInput(InputAction.CallbackContext context)
    {
        if (!_buffering && _firstAttacked)
        {
            if(context.action == _inputAction.action)
            {
                _firstCombo.Combo();
            }

            if (context.action == _inputAction2.action)
            {
                _secondCombo.Combo();
            }
        }
    }
    private void FirstAttack(InputAction.CallbackContext context)
    {
        if (!_firstAttacked)
        {
            if(context.action == _inputAction.action)
            {
                _firstCombo.Combo();
            }

            if (context.action == _inputAction2.action)
            {
                _secondCombo.Combo();
            }
        }
    }
    private void InputBuffer(InputAction.CallbackContext context)
    {
        if (_buffering && _firstAttacked && _canInput && _canAttack && (context.action == _inputAction.action || context.action == _inputAction2.action))
        {
            ResetTrigger();
           if(context.action == _inputAction.action)
           {
              _buffer =  _firstCombo;
           }

           if (context.action == _inputAction2.action)
           {
               _buffer =  _secondCombo;
           }
        }
    }

    public void StartBuffering()
    {
        _buffering = true;
    }

    public void ReleaseBuffer()
    {
        if (_buffer != null)
        {
            _buffer.Combo();
        }

        _buffering = false;
        ResetTrigger();
    }

    public void FirstAttacked()
    {
        _firstAttacked = true;
    }

    private void ResetTrigger()
    {
        _buffer = null;
        //_firstCombo.ResetComboTrigger();
        //_secondCombo.ResetComboTrigger();
    }

    public void ResetFirstAttackTimer()
    {
        _firstAttacked = true;
        StopAllCoroutines();
        if (_firstAttacked)
        {
            StartCoroutine(ResetFirstAttack());
        }
    }

    private IEnumerator ResetFirstAttack()
    {
        yield return new WaitForSeconds(_resetFirstAttackTimer);
        _buffer = null;
        //_firstCombo.ResetComboTrigger();
        //_secondCombo.ResetComboTrigger();
        _firstAttacked = false;
        Debug.Log("First Attack Reset");
    }
}

