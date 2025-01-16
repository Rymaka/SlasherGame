using UnityEngine;

public class CharacterStateController : MonoBehaviour
{
    [SerializeField] private string[] _characterStates;
    public bool _canWalk = true;
    public bool _canAttack = true;
    public bool _canFall = true;
    public bool _canTakeDamage = true;
    public bool _canCast = true;

    public void StateUpdate(string state)
    {
        switch (state)
        {
            case "Walking" :
                StateWalking();
                break;
            case "Attacking" :
                StateAttacking();
                break;
        }
    }

    private void StateWalking()
    { 
        ResetToDefaultState();
    }

    private void StateAttacking()
    {
        ResetToDefaultState();
        _canWalk = false;
        _canCast = false;
    }
    
    private void ResetToDefaultState()
    {
        _canWalk = true;
        _canAttack  = true;
        _canFall = true;
        _canTakeDamage = true;
        _canCast = true;
    }
}
