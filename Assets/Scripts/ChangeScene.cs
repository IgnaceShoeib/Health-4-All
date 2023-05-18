using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    void OnTriggerEnter()
    {
	    SceneChange(sceneName);
    }
    public void SceneChange(string scene)
    {
	    SceneManager.LoadScene(scene);
    }
}