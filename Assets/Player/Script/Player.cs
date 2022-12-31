using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Player : MonoBehaviour
{
    public GameObject in_game_crosshair;
    public GameObject hook;
    public GameObject crosshair;

    public GameObject canon_obj;

    public string horizontal_axis;
    public string vertical_axis;
    public string fire_button;

    public LayerMask sea_plane_mask;

    public float min_x_aim;
    public float max_x_aim;
    public float min_y_aim;
    public float max_y_aim;

    public float speed;

    public int player_num;

    void Update()
    {
        PlayerIndex controllerNumber = PlayerIndex.One;
        GamePadState state = GamePad.GetState(controllerNumber);
        PlayerIndex controllerNumber2 = PlayerIndex.Two;
        GamePadState state2 = GamePad.GetState(controllerNumber2);

        //Move a ball in 2d space in front of the player
        if (player_num == 0)
        {
            in_game_crosshair.transform.localPosition += new Vector3(state.ThumbSticks.Left.X * speed, state.ThumbSticks.Left.Y * speed, 0.0f);
            Debug.Log(in_game_crosshair.transform.localPosition);
        }
        else
        {
            in_game_crosshair.transform.localPosition += new Vector3(state2.ThumbSticks.Left.X * speed, state2.ThumbSticks.Left.Y * speed, 0.0f);
        }

        ClampPosition();        

        //Project a vector from the ball to the direction that the camera is facing
        //It stops on the sea plane
        Vector3 hit_point;
        RaycastHit hit;
        Vector3 ray_dir = (in_game_crosshair.transform.position - Camera.main.transform.position).normalized;
        Debug.DrawRay(in_game_crosshair.transform.position, ray_dir * 100, Color.red, 10);
        if (Physics.Raycast(in_game_crosshair.transform.position, ray_dir, out hit, Mathf.Infinity, sea_plane_mask))
        {
            hit_point = hit.point;
            //Make the harpoon look towards the point of impact
            transform.LookAt(hit.point);
            crosshair.transform.position = hit_point;
            float dist_scale_multiplier = 0.025f;
            float final_scale = 0.32f + dist_scale_multiplier * Vector3.Distance(Camera.main.transform.position, crosshair.transform.position);
            crosshair.transform.localScale = new Vector3(final_scale, final_scale, final_scale);
        }

        if (player_num == 0 && state.Buttons.A == ButtonState.Pressed)
        {
            hook.GetComponent<Hook>().OnThrown(hit.point);
        }
        if (player_num == 1 && state2.Buttons.A == ButtonState.Pressed)
        {
            hook.GetComponent<Hook>().OnThrown(hit.point);
        }
    }

    private void ClampPosition()
    {
        if (in_game_crosshair.transform.localPosition.x < min_x_aim)
        {
            in_game_crosshair.transform.localPosition = new Vector3(
                min_x_aim,
                in_game_crosshair.transform.localPosition.y,
                in_game_crosshair.transform.localPosition.z);
        }
        if (in_game_crosshair.transform.localPosition.x > max_x_aim)
        {
            in_game_crosshair.transform.localPosition = new Vector3(
                max_x_aim,
                in_game_crosshair.transform.localPosition.y,
                in_game_crosshair.transform.localPosition.z);
        }
        if (in_game_crosshair.transform.localPosition.y < min_y_aim)
        {
            in_game_crosshair.transform.localPosition = new Vector3(
                in_game_crosshair.transform.localPosition.x,
                min_y_aim,
                in_game_crosshair.transform.localPosition.z);
        }
        if (in_game_crosshair.transform.localPosition.y > max_y_aim)
        {
            in_game_crosshair.transform.localPosition = new Vector3(
                in_game_crosshair.transform.localPosition.x,
                max_y_aim,
                in_game_crosshair.transform.localPosition.z);
        }
    }
}
