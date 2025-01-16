using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeComboController : MonoBehaviour
{
    [SerializeField] protected Animator _anim;
    [SerializeField] protected string _comboTrigger;
    public bool _canAttack = true;
    public bool _canInput = true;
    
    protected virtual void OnAwake()
    {
        if (_comboTrigger == null)
        {
            Debug.LogError("Melee Combo Controller has not been assigned.");
        }
    }

    private void Awake(){
        OnAwake();
    }
    
    public virtual void Combo()
    {
        if (_canAttack && _canInput)
        {
            _anim.SetTrigger(_comboTrigger);
        }
    }

    public void ResetComboTrigger()
    {
        _anim.ResetTrigger(_comboTrigger);
    }
}
