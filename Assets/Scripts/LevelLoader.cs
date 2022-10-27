using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string _levelId;

    public void LoadThisLevel()
    {
        var thisLevelName = SceneManager.GetActiveScene().name;
        LoadLevel(thisLevelName);
    }
    public void LoadLevel(string id)
    {
        SceneManager.LoadScene(id);
    }
    public void LoadLevel()
    {
        LoadLevel(_levelId);
    }
}
