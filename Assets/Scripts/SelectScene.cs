using UnityEngine;

public class SelectScene : MonoBehaviour
{
    public void SetLevel(int level)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }
}
