using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;
using UnityEngine.SceneManagement;

public class ManageScore : MonoBehaviour
{
    GameObject aux_gamesystem;
    GameObject TrashSpawner;
    // Start is called before the first frame update
    public Vector3 SpawnPos1;
    public Vector3 SpawnPos2;
    float startTime;
    public float waitTime=2;
    TrashSpawner classTrash;

    public Text Text1, Text2;
    float totalWeight = 0, totalWeight2 = 0;

    void Start()
    {
        aux_gamesystem = GameObject.Find("Game Manager");
        TrashSpawner = GameObject.Find("TrashSpawner");
        classTrash = TrashSpawner.GetComponent<TrashSpawner>();
        classTrash.spawn_trash = false;

        aux_gamesystem.GetComponent<GameSystem>().player_trash[0].Add(0);
        aux_gamesystem.GetComponent<GameSystem>().player_trash[1].Add(0);
  
        startTime = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerIndex controllerNumber = PlayerIndex.One;
        GamePadState state = GamePad.GetState(controllerNumber);
        PlayerIndex controllerNumber2 = PlayerIndex.Two;
        GamePadState state2 = GamePad.GetState(controllerNumber2);

        if (Time.fixedTime-startTime>= waitTime)
        {
            Debug.Log(aux_gamesystem.GetComponent<GameSystem>().player_trash[0].Count);
            if(aux_gamesystem.GetComponent<GameSystem>().player_trash[0].Count>0)
            {
                GameObject createdObject = Instantiate(classTrash.trash_prefabs[aux_gamesystem.GetComponent<GameSystem>().player_trash[0][0]], SpawnPos1, Quaternion.identity);
                Rigidbody obje_rb = createdObject.GetComponent<Rigidbody>();
                obje_rb.useGravity = true;
                obje_rb.constraints = RigidbodyConstraints.None;
                totalWeight += classTrash.trash_weight[aux_gamesystem.GetComponent<GameSystem>().player_trash[0][0]];
                Text1.text = totalWeight.ToString() + " Kg";
                aux_gamesystem.GetComponent<GameSystem>().player_trash[0].RemoveAt(0);

            }
            if (aux_gamesystem.GetComponent<GameSystem>().player_trash[1].Count > 0)
            {
                GameObject createdObject = Instantiate(classTrash.trash_prefabs[aux_gamesystem.GetComponent<GameSystem>().player_trash[1][0]], SpawnPos2, Quaternion.identity);
                Rigidbody obje_rb = createdObject.GetComponent<Rigidbody>();
                obje_rb.useGravity = true;
                obje_rb.constraints = RigidbodyConstraints.None;
                totalWeight2 += classTrash.trash_weight[aux_gamesystem.GetComponent<GameSystem>().player_trash[1][0]];
                Text2.text = totalWeight2.ToString() + " Kg";
                aux_gamesystem.GetComponent<GameSystem>().player_trash[1].RemoveAt(0);


            }
            waitTime -= 0.2f;
            startTime = Time.fixedTime;
            

        }
        if(state.Buttons.A == ButtonState.Pressed
            || state2.Buttons.A == ButtonState.Pressed)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
