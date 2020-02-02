using UnityEngine;

public class CloudSystem : MonoBehaviour
{

    public float maxPlayerOffset;
    public float minSpeed;
    public float maxSpeed;

    private Transform player;

    private Transform[] clouds;
    private float[] speed;

    void Start()
    {
        clouds = new Transform[transform.childCount];
        speed = new float[transform.childCount];
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i] = transform.GetChild(i);
            speed[i] = Random.Range(minSpeed, maxSpeed);
            if (Random.value > .5f)
                speed[i] = -speed[i];
        }
    }

    void Update()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player")?.transform;

        var playerPos = player == null ? 0f : player.transform.position.x;

        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].position += Vector3.right * (speed[i] * Time.deltaTime);

            var dist = clouds[i].position.x - playerPos;
            if (dist > maxPlayerOffset)
            {
                var newXPos = playerPos - 0.99f * maxPlayerOffset;
                var newPos = clouds[i].position;
                newPos.x = newXPos;
                clouds[i].position = newPos;
            }
            else if (dist < -maxPlayerOffset)
            {
                var newXPos = playerPos + 0.99f * maxPlayerOffset;
                var newPos = clouds[i].position;
                newPos.x = newXPos;
                clouds[i].position = newPos;
            }
        }


    }
}
