using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] ParticleSystem upgradeParticle;
    [SerializeField] ParticleSystem downgradeParticle;

    protected Player player;
    protected UnityEvent EndSkillEvent = new UnityEvent();
    
    protected abstract void BasicSkill();
    protected abstract void CancelSkill();

    bool skilling = false;

    private void Awake()
    {
        player = GetComponent<Player>();
        EndSkillEvent.AddListener(() => CancelSkill());

        StartSkill();
    }

    private void SkillTimer()
    {
        StartCoroutine(SkillCoolTime());
    }
    private IEnumerator SkillCoolTime()
    {
        yield return new WaitForSeconds(player.GetPlayer.skillCoolTime);
        while(true)
        {
            yield return new WaitForSeconds(player.GetPlayer.skillCoolTime);
            skilling = true;
            SoundManager.Instance.PlaySound(SoundManager.Sound.UpgradePlayerSound);
            upgradeParticle.Play();
            BasicSkill();

            yield return new WaitForSeconds(3f);

            EndSkillEvent?.Invoke();
            downgradeParticle.Play();
            skilling = false;
        }
    }

    private void StartSkill()
    {
        SkillTimer();
    }

    private void StopSkill()
    {
        StopCoroutine(SkillCoolTime());
    }
}
