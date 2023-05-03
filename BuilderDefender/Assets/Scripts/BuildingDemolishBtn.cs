using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishBtn : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(transform.parent.gameObject);
        });
    }
}
