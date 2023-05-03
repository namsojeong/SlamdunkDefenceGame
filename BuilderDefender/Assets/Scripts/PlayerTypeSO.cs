using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerType")]
public class PlayerTypeSO : ScriptableObject
{
    public GameObject pf;
    public string nameString;
    public Sprite character;
    public int maxHP;

    public int skillCoolTime;

    public PowerType powerType;
    public ResourceAmount[] constructionResourceCostArray;
}
