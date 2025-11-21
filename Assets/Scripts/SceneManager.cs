using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void GoToEarth()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
