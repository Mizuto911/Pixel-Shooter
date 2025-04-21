using UnityEngine;

public class BossPhase3 : StateMachineBehaviour
{
    BossScript bossScript;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossScript = animator.GetComponent<BossScript>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 1)
        {
            bossScript.spawnEnemies = true;
        }
        
        if (bossScript.enemiesKilled >= bossScript.enemiesToKill)
        {
            animator.SetTrigger("Return3");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossScript.enemiesKilled = 0;
        animator.ResetTrigger("Return3");
    }
}
