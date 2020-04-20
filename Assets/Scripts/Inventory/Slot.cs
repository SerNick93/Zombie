using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Slot : MonoBehaviour
{
    [SerializeField]
    Image icon;
    [SerializeField]
    TextMeshProUGUI stackCount;

    public Image Icon { get => icon; set => icon = value; }
    public TextMeshProUGUI StackCount { get => stackCount; set => stackCount = value; }

    // Start is called before the first frame update
    void Start()
    {
        Icon = GetComponentInChildren<Image>();
        StackCount = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
