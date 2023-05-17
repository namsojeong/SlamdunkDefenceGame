using DG.Tweening;
using System;
using UnityEngine;
using static SoundManager;

[RequireComponent(typeof(DragObject))]
public class UpgradeBuilding : MonoBehaviour
{
    [SerializeField] ParticleSystem mergeParticle;
    [SerializeField] PowerType powerType;

    private Vector2 originScale = new Vector2(1f, 1f);
    private int level = 1;
    private const int MAX_LEVEL = 3;
    public int GetLevel { get { return level; } }

    private Building building;
    DragObject drag;
    UpgradeBuilding mergeObj;
    Player player;

    private void Awake()
    {
        drag = GetComponent<DragObject>(); 
        building = GetComponent<Building>();
        drag.OnEndEvent.AddListener(() => Merge());

        transform.localScale = originScale;
    }

    public void Merge()
    {
        if (mergeObj == null) return;
        if (level >= MAX_LEVEL || mergeObj.level != level) return;

        level = Mathf.Clamp(level + mergeObj.GetLevel, 1, MAX_LEVEL);

        building.GetBuilding.resourceGeneratorData.timerMax -= 0.2f;
        Mathf.Clamp(building.GetBuilding.resourceGeneratorData.timerMax, 0.5f, 1f);

        transform.DOScale(originScale * new Vector2(level, level), 1f).SetEase(Ease.OutBounce);

        SoundManager.Instance.PlaySound(Sound.UpgradeSound);
        mergeParticle.Play();
        mergeObj.DeleteBuild();
    }

    public void DeleteBuild()
    {
        Destroy(gameObject);
    }

    private void AddPlayer()
    {
        if (level < MAX_LEVEL) return;
        player.AddAbillity(powerType, 3f);

        mergeParticle.Play();
        Destroy(gameObject, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
     if(other.gameObject.layer ==  LayerMask.NameToLayer("Building"))
        {
            mergeObj = other.GetComponent<UpgradeBuilding>();
        }

     if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player= other.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer ==  LayerMask.NameToLayer("Building"))
        {
            mergeObj = null;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = null;
        }
    }

    private void OnMouseUp()
    {
        if(player != null) { AddPlayer(); }
    }
}
