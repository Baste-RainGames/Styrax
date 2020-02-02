using UnityEngine;

public class ParallaxXY : MonoBehaviour
{
    public float parallaxWidth;
    public float parallaxHeight;

    public Transform player;
    public Transform parallaxMain;
    public Transform parallaxHelperX;
    public Transform parallaxHelperY;
    public Transform parallaxHelperDiag;

    private Vector3 lastPlayerPos;

    void Update()
    {
        if (player == null)
            return;

        var playerPos = player.position;
        var playerMovement = playerPos - lastPlayerPos;
        playerMovement.z = 0;
        lastPlayerPos = playerPos;

        parallaxMain.position += playerMovement * Settings.Parallax;

        HandleXOffset(playerPos);
        HandleYOffset(playerPos);

        parallaxHelperDiag.position = new Vector3(parallaxHelperX.position.x, parallaxHelperY.position.y, parallaxHelperDiag.position.z);
    }

    void HandleXOffset(Vector2 playerPos)
    {
        var xOffset = playerPos.x - parallaxMain.position.x;
        if (xOffset > parallaxWidth / 2f)
        {
            parallaxMain.position += new Vector3(parallaxWidth, 0f, 0f);
        }
        else if (xOffset < -parallaxWidth / 2f)
        {
            parallaxMain.position -= new Vector3(parallaxWidth, 0f, 0f);
        }

        if (playerPos.x > parallaxMain.position.x)
        {
            parallaxHelperX.position = parallaxMain.position + new Vector3(parallaxWidth, 0f, 0f);
        }
        else
        {
            parallaxHelperX.position = parallaxMain.position - new Vector3(parallaxWidth, 0f, 0f);
        }
    }

    void HandleYOffset(Vector2 playerPos)
    {
        var yOffset = playerPos.y - parallaxMain.position.y;
        if (yOffset > parallaxHeight / 2f)
        {
            parallaxMain.position += new Vector3(0f, parallaxHeight, 0f);
        }
        else if (yOffset < -parallaxHeight / 2f)
        {
            parallaxMain.position -= new Vector3(0f, parallaxHeight, 0f);
        }

        if (playerPos.y > parallaxMain.position.y)
        {
            parallaxHelperY.position = parallaxMain.position + new Vector3(0f, parallaxHeight, 0f);
        }
        else
        {
            parallaxHelperY.position = parallaxMain.position - new Vector3(0f, parallaxHeight, 0f);
        }
    }
}