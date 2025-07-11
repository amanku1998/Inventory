using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameService : GenericMonoSingleton<GameService>
{
    [SerializeField] private TextMeshProUGUI currentCoins;
    [SerializeField] private TextMeshProUGUI currentItemWeight;
    [SerializeField] private Button addRandomItems;

    [SerializeField] private Transform uiCanvasTransform;

    //[SerializeField] private 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
