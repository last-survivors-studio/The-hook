using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum TrashEnum
//{
//    can = 0,    // 0,1 kg
//    pillow,     // 0,3 kg
//    boot,       // 0,5 kg
//    chair,      //12   kg
//    tv,         //30   kg
//    armchair,   //40   kg
//    sofa,       //60   kg
//    //washing machine
//};



public class Trash : MonoBehaviour
{
    enum TrashState
    {
        FLOATING, DISPUTED, ACHIEVED, RECOVERING
    };

    
    public float kg;
    private float duel_time = 3.5F;

    private bool player_0_detected = false;
    private bool player_1_detected = false;
    private int player_0_hits = 0;
    private int player_1_hits = 0;
    public int TrashNum = 0;

    private TrashState state = TrashState.FLOATING;

    public GameObject hook_object;

    private void Update()
    {
        if (state == TrashState.DISPUTED)
        {
            if (Input.GetButtonDown("Fire1") == true)
            {
                ++player_0_hits;
            }
            if (Input.GetButtonDown("Fire2") == true)
            {
                ++player_1_hits;
            }
        }
        else if(state == TrashState.RECOVERING || state == TrashState.ACHIEVED)
        {
            transform.position = hook_object.transform.position;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if ( collider.gameObject.tag == "Arrow" && state != TrashState.ACHIEVED)
        {
            Hook hook = collider.gameObject.GetComponent<Hook>();
            
            if (hook.player_num == 0)
            {
                state = TrashState.RECOVERING;
                player_0_detected = true;
            }
            else if  (hook.player_num == 1)
            {
                state = TrashState.RECOVERING;
                player_1_detected = true;
            }

            if (player_0_detected && player_1_detected)
            {
                state = TrashState.DISPUTED;
                Invoke("FinishDuel", duel_time);
                //GameObject.Find("player_1").isStatic = true;
                //GameObject.Find("player_2").isStatic = true;
            }
            hook_object = collider.gameObject;
        }
    }

   public void FinishDuel()
    {
        state = TrashState.ACHIEVED;
        
        if( player_0_hits > player_1_hits)
        {
            hook_object = GameObject.Find("player_1");
            Debug.Log("player 1 win");
        }
        else if (player_0_hits < player_1_hits)
        {
            hook_object = GameObject.Find("player_2");
            Debug.Log("player 2 win");
        }
        else
        {
            float random = Random.Range(0, 1);

            if (random >= 0.5f)
            {
                hook_object = GameObject.Find("player_1");
            }
            else
            {
                hook_object = GameObject.Find("player_2");
            }

            Debug.Log("EMPATE");
        }

        GameObject.Find("player_1").isStatic = false;
        GameObject.Find("player_2").isStatic = false;
    }


}
