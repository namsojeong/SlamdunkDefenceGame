using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance { get; private set; }

    private PlayerTypeList playerList;

    private Transform content;
    private Transform shopTemplate;
    private Transform expensiveTooltip;

    private void Awake()
    {
        Instance = this;

        playerList = (PlayerTypeList)Resources.Load(typeof(PlayerTypeList).Name);

        content = transform.Find("Content");
        shopTemplate = content.GetChild(0);
        shopTemplate.gameObject.SetActive(false);

        expensiveTooltip = transform.Find("expensiveTooltip");

        SetPlayer();
    }

    private void SetPlayer()
    {
        foreach(PlayerTypeSO player in playerList.list)
        {
            Transform shopTemp = Instantiate(shopTemplate, content.transform);
            shopTemp.Find("sprite").GetComponent<Image>().sprite = player.character;

                shopTemp.transform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(player.constructionResourceCostArray[0].amount.ToString());

            Button purchaseBtn = shopTemp.Find("purchaseButton").GetComponent<Button>();
            purchaseBtn.onClick.AddListener(() =>
            {
                PurchaseComparison(player);
            });
            purchaseBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(player.nameString);

            shopTemp.gameObject.SetActive(true);
        }
    }

    private void PurchaseComparison(PlayerTypeSO player)
    {
        bool canBuy = false;
        for(int i=0;i< player.constructionResourceCostArray.Length;i++)
        {
            int money = ResourceManager.Instance.GetResourceAmount(player.constructionResourceCostArray[i].resourceType);
            canBuy = money > player.constructionResourceCostArray[i].amount;
            if (!canBuy) break;
        }

        if (canBuy) Buy(player);
        else Expensive();

    }

    private void Expensive()
    {
        expensiveTooltip.gameObject.SetActive(true);
        StartCoroutine(DelayExpensiveTooltip());
    }

    private IEnumerator DelayExpensiveTooltip()
    {
        yield return new WaitForSecondsRealtime(1f);
        expensiveTooltip.gameObject.SetActive(false);
        Visible(false);
    }

    private void Buy(PlayerTypeSO player)
    {
        ResourceManager.Instance.SpendResources(player.constructionResourceCostArray);
        player.constructionResourceCostArray[0].amount *= 2;
        PutPlayer(player);
        Visible(false);
    }

    private void PutPlayer(PlayerTypeSO player)
    {
        Player _player = CreatePlayer(player, Vector2.zero);
        _player.gameObject.SetActive(true);
    }
    public Player CreatePlayer(PlayerTypeSO _player, Vector2 position)
    {
        Transform playerTransform = Instantiate(_player.pf.transform, position, Quaternion.identity);
        return playerTransform.GetComponent<Player>();
    }

    public void Visible(bool visible)
    {
        gameObject.SetActive(visible);

        if (visible)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

}
