using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager1 : MonoBehaviour
{
    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
