using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayer : MonoBehaviour
{
    private PlayerTypeList playerList;

    private Transform content;
    private Transform selectTemplate;

    private void Awake()
    {
        playerList = (PlayerTypeList)Resources.Load(typeof(PlayerTypeList).Name);

        content = transform.Find("Content");
        selectTemplate = content.GetChild(0);
        selectTemplate.gameObject.SetActive(false);

        SetPlayer();

        Visible(true);
    }

    private void SetPlayer()
    {
        foreach (PlayerTypeSO player in playerList.list)
        {
            Transform selectTemp = Instantiate(selectTemplate, content.transform);
            selectTemp.Find("sprite").GetComponent<Image>().sprite = player.character;

            Button purchaseBtn = selectTemp.Find("selectButton").GetComponent<Button>();
            purchaseBtn.onClick.AddListener(() =>
            {
                Select(player);
            });
            purchaseBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(player.nameString);

            selectTemp.gameObject.SetActive(true);
        }
    }

    private void Select(PlayerTypeSO player)
    {
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
