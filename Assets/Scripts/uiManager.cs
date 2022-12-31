using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    private float started_at;
    [SerializeField]
    private Text associatedText;
    [SerializeField]
    private GameSystem gameSysScript;


    // Start is called before the first frame update
    void Start()
    {
        started_at = Time.time;

        // load game system script reference handler
        gameSysScript = FindObjectOfType<GameSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        associatedText.text = "countdown: " +  (gameSysScript.match_time - System.Math.Round((Time.time - started_at), 0));
    }
}
