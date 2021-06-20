using UnityEngine;
using UnityEngine.SceneManagement;
    
public class LoadScene : MonoBehaviour
{
    public void NextScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}