using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum HookState
{
    PICKED,
    RECOVER,
    THROWN
};

public class Hook : MonoBehaviour
{
    public GameObject canon;
    public GameObject canonEnd;
    public float forcePower = 1.0f;
    public int player_num;
   

    private float last_shot_time = 0.0f;
    private Vector3 destination_pos;

    public ParticleSystem water_splash;
    public ParticleSystem harpon_air;

    [Tooltip("Maximum time the hook travels before returning to the player.")]
    public float max_return_time = 0.0f;

    HookState state = HookState.PICKED;
    Rigidbody hookRigidbody;

    private Vector3 shot_dir;

    private GameSystem game_system;

    public AudioSource water_fx;
    public AudioSource hook_fx;

    private void Start()
    {
        game_system = FindObjectOfType<GameSystem>();
        hookRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //When it returns, it gets put as a child and that moves it with the hook
        if (state == HookState.PICKED)
        {
            hookRigidbody.velocity = Vector3.zero;
            transform.position = canonEnd.transform.position;
            transform.rotation = canonEnd.transform.rotation;
        }
        if (state == HookState.THROWN)
        {
            Throwing();
        }
        else if(state == HookState.RECOVER)
        {
            Recovering();
            float distance = (canonEnd.transform.position - transform.position).magnitude;
            if(distance < hookRigidbody.velocity.magnitude * Time.deltaTime)
            {
                state = HookState.PICKED;
                //Set the position and make it a child
                transform.SetParent(canon.transform);
                hookRigidbody.velocity = Vector3.zero;
                hookRigidbody.angularVelocity = Vector3.zero;
            }

        }
    }

    public void OnThrown(Vector3 dst_pos)
    {
        transform.SetParent(null);
        shot_dir = (dst_pos - canonEnd.transform.position).normalized;
        if (state == HookState.PICKED)
        {
            ParticleSystem new_harpon;
            new_harpon = Instantiate(harpon_air, canonEnd.transform.position, canonEnd.transform.rotation);
            new_harpon.transform.Rotate(new_harpon.transform.rotation.eulerAngles.x, 90, new_harpon.transform.rotation.eulerAngles.z);

            new_harpon.Play();
            hook_fx.Play();

            destination_pos = dst_pos;
            state = HookState.THROWN;
        }
        last_shot_time = Time.time;
    }

    void Throwing()
    {
        hookRigidbody.AddForce(shot_dir * forcePower * Time.deltaTime, ForceMode.Force);

        //Force the hook to return if it passes max_return time
        if (Time.time - last_shot_time > max_return_time)
        {
            OnImpact();
        }
    }

    void OnImpact()
    {
        if(state == HookState.THROWN)
        {
            ParticleSystem new_water;
            new_water = Instantiate(water_splash, destination_pos, Quaternion.Euler(-90,0,0));

            new_water.Play();
            water_fx.Play();

            state = HookState.RECOVER;
        }

        hookRigidbody.velocity = Vector3.zero;
        hookRigidbody.angularVelocity = Vector3.zero;
    }

    void Recovering()
    {
        Vector3 direction = (canonEnd.transform.position - transform.position).normalized;
        hookRigidbody.AddForce(direction * forcePower * Time.deltaTime, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Trash") || other.gameObject.CompareTag("Ground"))
        {
            OnImpact();
        }
        if (other.gameObject.CompareTag("SavedWall"))
        {
            state = HookState.PICKED;
        }
    }
}
