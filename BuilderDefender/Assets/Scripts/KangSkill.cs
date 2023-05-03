using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KangSkill : Skill
{

    [SerializeField] float plusScale = 3f;

    protected override void BasicSkill()
    {
        player.AddScale(plusScale);
    }

    protected override void CancelSkill()
    {
        player.AddScale(-plusScale);
        
    }
}
