using JetBrains.Annotations;
using UnityEngine;

public partial class Enemy
{

    
    [UsedImplicitly]
    public void AnimationEvent_AttackBegin(){
        Debug.Log("Attack Started");
        hitbox.ActivateHitbox();
    }
    
    [UsedImplicitly]
    public void AnimationEvent_AttackEnd()
    {
        Debug.Log("Attack Ended");
        hitbox.DeactivateHitbox();
    }
    
}
