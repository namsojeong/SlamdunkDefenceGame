using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeoSkill : Skill
{
  
    [SerializeField] float range = 4f;

    protected override void BasicSkill()
    {
        player.AddRange(range);
    }

    protected override void CancelSkill()
    {
        player.AddRange(-range);
    }
}
