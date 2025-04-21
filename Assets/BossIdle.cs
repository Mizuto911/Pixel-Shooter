using UnityEngine;

public class BossIdle : StateMachineBehaviour
{
    BossScript bossScript;
    string trigger = null;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossScript = animator.GetComponent<BossScript>();
        bossScript.currentAttack++;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (bossScript.currentAttack)
        {
            case 1:
                trigger = "Pattern1";
                break;
            case 2:
                trigger = "Pattern2";
                break;
            case 3:
                trigger = "Pattern3";
                break;
            default:
                bossScript.currentAttack = 1;
                break;
        }

        if (trigger != null)
        {
            animator.SetTrigger(trigger);
        }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(trigger);
        trigger = null;
    }
}
