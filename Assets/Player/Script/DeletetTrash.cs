using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletetTrash : MonoBehaviour
{
    public GameObject gameSystem;
    public int player;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Trash"))
        {
            gameSystem.GetComponent<GameSystem>().player_trash[player].Add(collider.transform.gameObject.GetComponent<Trash>().TrashNum);
            Destroy(collider.gameObject);
        }
    }
}
