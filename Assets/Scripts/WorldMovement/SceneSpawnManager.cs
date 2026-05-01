using UnityEngine;

public class SceneSpawnManager : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        float x = PlayerPrefs.GetFloat("spawnX", player.position.x);
        float y = PlayerPrefs.GetFloat("spawnY", player.position.y);

        player.position = new Vector3(x, y, 0);
    }
}