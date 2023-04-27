using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    void OnTriggerEnter()
    {
	    SceneManager.LoadScene(sceneName);
    }
}