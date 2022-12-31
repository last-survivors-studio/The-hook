using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public List<GameObject> trash_prefabs;
    public List<float> trash_weight;

    public GameObject min_spawn_pos;
    public GameObject max_spawn_pos;

    //Instantiate objects every x time
    public float spawn_time_default;
    public float spawn_time_last_moments;
    private float curr_spawn_time = 0.0f;

    private float last_instantiate_time = 0.0f;

    public float trash_velocity;

    private float instance_pos_y = 0.0f;

    public float max_rotation_speed;

    private GameSystem game_system;

    public bool spawn_trash = true;

    enum SpawnPosition
    {
        top = 0,
        down,
        left,
        right,
        max
    };

    private void Start()
    {
        //INFO: Can do this because there will only be one GameSystem in the game
        game_system = FindObjectOfType<GameSystem>();
        curr_spawn_time = spawn_time_default;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {if (spawn_trash)
        {
            if (Time.time > last_instantiate_time + curr_spawn_time)
            {
                //Instance
                int prefab_num = Random.Range(0, trash_prefabs.Count);
                GameObject trash_instance = Instantiate(trash_prefabs[prefab_num]);
                trash_instance.GetComponent<Trash>().TrashNum = prefab_num;
                //Position and movement
                Rigidbody trash_rb = trash_instance.GetComponent<Rigidbody>();
                int spawn_type = Random.Range((int)SpawnPosition.top, (int)SpawnPosition.max);
                float prefabMass = 1.0f;//trash_rb.mass;
                switch (spawn_type)
                {
                    case (int)SpawnPosition.top:
                        trash_instance.transform.position = new Vector3(
                            Random.Range(min_spawn_pos.transform.position.x, max_spawn_pos.transform.position.x),
                            instance_pos_y,
                            max_spawn_pos.transform.position.z);
                        trash_rb.velocity = new Vector3(
                            Random.Range(-1.0f, 1.0f),
                            0.0f,
                            Random.Range(0.1f, -1.0f));
                        trash_rb.velocity = trash_rb.velocity.normalized * trash_velocity * prefabMass;
                        break;

                    case (int)SpawnPosition.down:
                        trash_instance.transform.position = new Vector3(
                            Random.Range(min_spawn_pos.transform.position.x, max_spawn_pos.transform.position.x),
                            instance_pos_y,
                            min_spawn_pos.transform.position.z);
                        trash_rb.velocity = new Vector3(
                            Random.Range(-1.0f, 1.0f),
                            0.0f,
                            Random.Range(0.1f, 1.0f));
                        trash_rb.velocity = trash_rb.velocity.normalized * trash_velocity * prefabMass;
                        break;

                    case (int)SpawnPosition.right:
                        trash_instance.transform.position = new Vector3(
                            max_spawn_pos.transform.position.x,
                            instance_pos_y,
                            Random.Range(min_spawn_pos.transform.position.z, max_spawn_pos.transform.position.z));
                        trash_rb.velocity = new Vector3(
                            Random.Range(0.0f, -1.0f),
                            0.0f,
                            Random.Range(-1.0f, 1.0f));
                        trash_rb.velocity = trash_rb.velocity.normalized * trash_velocity * prefabMass;
                        break;

                    case (int)SpawnPosition.left:
                        trash_instance.transform.position = new Vector3(
                            min_spawn_pos.transform.position.x,
                            instance_pos_y,
                            Random.Range(min_spawn_pos.transform.position.z, max_spawn_pos.transform.position.z));
                        trash_rb.velocity = new Vector3(
                            Random.Range(0.0f, 1.0f),
                            0.0f,
                            Random.Range(-1.0f, 1.0f));
                        trash_rb.velocity = trash_rb.velocity.normalized * trash_velocity * prefabMass;
                        break;
                }

                //Rotation
                trash_instance.transform.rotation = Random.rotation;

                //Rotation speed
                trash_rb.angularVelocity = new Vector3(
                    Random.Range(-1.0f, 1.0f),
                    Random.Range(-1.0f, 1.0f),
                    Random.Range(-1.0f, 1.0f)).normalized
                    * Random.Range(0.0f, max_rotation_speed);

                last_instantiate_time = Time.time;
            }
        }
    }

    public void ChangeToLastMoments()
    {
        curr_spawn_time = spawn_time_last_moments;
    }

}
