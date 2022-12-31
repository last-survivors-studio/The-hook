using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Cotrols
//- Transitions between scenes

public class GameSystem : MonoBehaviour
{
    public AudioClip music1;
    public AudioClip music2;

    [SerializeField]
    private Camera mainCam;

    [Tooltip("Time that the match lasts in seconds")]
    public float match_time;

    [Tooltip("When does the last moments start")]
    public float last_moments_start_time;

    private float start_time;

    private bool last_moments = false;

    private TrashSpawner trash_spawner;
    private WaterNoise water_noise;

    private Animation_Boot ship_turbulence;

    public  List<int>[] player_trash;//There are two players

    private void Awake()
    {

        if(player_trash==null)
        {
            player_trash = new List<int>[2];
            player_trash[0] = new List<int>();
            player_trash[1] = new List<int>();
        }
        DontDestroyOnLoad(this.gameObject);

        ship_turbulence = FindObjectOfType<Animation_Boot>();
    }

    private void Start()
    {
        last_moments = false;

        start_time = Time.time;
        trash_spawner = FindObjectOfType<TrashSpawner>();
        water_noise = FindObjectOfType<WaterNoise>();

        mainCam.GetComponent<AudioSource>().clip = music1;
        mainCam.GetComponent<AudioSource>().Play();

    }

    void Update()
    {
        if (!last_moments
            && Time.time > last_moments_start_time - start_time)
        {
            ChangeToLastMoments();
            last_moments = true;
        }
        if (Time.time - start_time > match_time)
        {
            //End game and transition to count point screen
            //TODO: Play SFX
            SceneManager.LoadScene(2);//2 is win scene
        }
    }

    private void ChangeToLastMoments()
    {
        trash_spawner.ChangeToLastMoments();
        water_noise.power = 1.0f;
        water_noise.scale = 0.98f;
        water_noise.timeScale = 0.4f;
        water_noise.waveForce = 3.0f;

        mainCam.GetComponent<AudioSource>().clip = music2;
        mainCam.GetComponent<AudioSource>().Play();

        ship_turbulence.maxSize = 10.72f;
        ship_turbulence.rotation_speed = 4.5f;
    }
}
