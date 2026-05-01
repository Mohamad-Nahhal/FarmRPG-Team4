using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Door : Interactable
{
    [SerializeField] private string doorID; 
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Vector2 spawnPositionInNextScene;

    public override void Interact(PlayerController player)
    {
        // Save where we came from (THIS door)
        PlayerPrefs.SetString("lastDoorID", doorID);

        PlayerPrefs.SetFloat("returnX", player.transform.position.x);
        PlayerPrefs.SetFloat("returnY", player.transform.position.y);
        PlayerPrefs.SetString("returnScene", SceneManager.GetActiveScene().name);

        // Save where we will spawn in next scene
        PlayerPrefs.SetFloat("spawnX", spawnPositionInNextScene.x);
        PlayerPrefs.SetFloat("spawnY", spawnPositionInNextScene.y);

        SceneLoader.Instance.LoadScene(sceneToLoad);
    }

    
}