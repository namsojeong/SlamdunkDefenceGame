using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSkill : Skill
{
    [SerializeField] float plusSpeed = 20f;

    protected override void BasicSkill()
    {
        player.AddSpeed(plusSpeed);
    }

    protected override void CancelSkill()
    {
        player.AddSpeed(-plusSpeed);
    }
}
