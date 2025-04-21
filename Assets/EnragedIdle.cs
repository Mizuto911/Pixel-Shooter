using UnityEngine;

public class EnragedIdle : StateMachineBehaviour
{
    BossScript bossScript;
    string trigger = null;
    float timer = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossScript = animator.GetComponent<BossScript>();
        bossScript.currentAttack++;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timer <= 1f)
        {
            timer += Time.deltaTime;
            return;
        }
        else
        {
            timer = 0;
        }
        switch (bossScript.currentAttack)
        {
            case 1:
                trigger = "EnragedPattern1";
                break;
            case 2:
                trigger = "EnragedPattern2";
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
