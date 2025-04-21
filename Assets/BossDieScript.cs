using UnityEngine;

public class BossDieScript : StateMachineBehaviour
{
    GameObject boss;
    BossScript bossScript;
    GameLogic logic;
    GameObject smoke;
    AudioManager audioManager;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        logic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        boss = animator.gameObject;
        bossScript = boss.GetComponent<BossScript>();
        smoke = Instantiate(bossScript.bossDamage, boss.transform.position, bossScript.bossDamage.transform.rotation,
            boss.transform);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 1)
        {
            logic.gameComplete = true;
            audioManager.playSFX(audioManager.playerDeath);
            audioManager.musicSource.Stop();
            Destroy(smoke);
            Instantiate(bossScript.bossExplosion, boss.transform.position, bossScript.bossExplosion.transform.rotation);
            Destroy(boss);
            for (int i = 0; i < 20; i++)
            {
                logic.addScore();
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
