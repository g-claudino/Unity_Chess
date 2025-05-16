using UnityEngine;

public class EyeMonsterAnimationController
{
    
    private readonly int IsAttackingParam = Animator.StringToHash("IsAttacking");
    private readonly int MoveSpeedParam = Animator.StringToHash("MoveSpeed");
    private Animator animator;
    
    public EyeMonsterAnimationController(Animator animator)
    {
        this.animator = animator;
    }

    public void SetIsAttacking(bool isAttacking)
    {
        animator.SetBool(IsAttackingParam, isAttacking);
    }

    public bool GetIsAttacking() => animator.GetBool(IsAttackingParam);
    
    public void SetMoveSpeed(float moveSpeed)
    {
        animator.SetFloat(MoveSpeedParam, moveSpeed);
    }
}
