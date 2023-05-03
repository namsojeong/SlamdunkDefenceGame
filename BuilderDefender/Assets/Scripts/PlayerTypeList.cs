using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerTypeList")]
public class PlayerTypeList : ScriptableObject
{
    public List<PlayerTypeSO> list;
}
