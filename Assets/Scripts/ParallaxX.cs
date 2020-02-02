using UnityEngine;

public class ParallaxX : MonoBehaviour
{
    public SpriteRenderer background;
    private Transform parallaxMain;
    private Transform parallaxHelperX;

    private Transform player;
    private float lastPlayerXPos;
    private float parallaxWidth;

    private void Start()
    {
        parallaxWidth = (background.sprite.texture.width * background.transform.localScale.x) / background.sprite.pixelsPerUnit;
        parallaxMain = background.transform;
        parallaxHelperX = Instantiate(parallaxMain, transform, true);
        Destroy(parallaxHelperX.GetComponent<ParallaxX>());
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player")?.transform;
            if (player != null)
                lastPlayerXPos = player.position.x;
        }

        if (player == null)
            return;

        var playerXPos = player.position.x;
        var playerXMovement = playerXPos - lastPlayerXPos;
        lastPlayerXPos = playerXPos;

        parallaxMain.position += new Vector3(playerXMovement * Settings.Parallax, 0f, 0f);

        var xOffset = playerXPos - parallaxMain.position.x;
        if (xOffset > parallaxWidth / 2f)
        {
            parallaxMain.position += new Vector3(parallaxWidth, 0f, 0f);
        }
        else if (xOffset < -parallaxWidth / 2f)
        {
            parallaxMain.position -= new Vector3(parallaxWidth, 0f, 0f);
        }

        if (playerXPos > parallaxMain.position.x)
        {
            parallaxHelperX.position = parallaxMain.position + new Vector3(parallaxWidth, 0f, 0f);
        }
        else
        {
            parallaxHelperX.position = parallaxMain.position - new Vector3(parallaxWidth, 0f, 0f);
        }
    }
}