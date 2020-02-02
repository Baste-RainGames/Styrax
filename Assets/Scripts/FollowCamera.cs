using UnityEngine;

[ExecuteAlways]
public class FollowCamera : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        SearchForPlayer();
    }

    private void Update()
    {
        SearchForPlayer();
    }

    private void SearchForPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player")?.transform;
            if (player != null)
            {
                var targetPos = player.position;
                targetPos.z = transform.position.z;
                transform.position = targetPos;
            }
        }
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        var targetPos = player.position;
        targetPos.z = transform.position.z;

        if (Application.isPlaying)
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * Settings.CameraFollowSpeed);
        else
            transform.position = targetPos;
    }
}