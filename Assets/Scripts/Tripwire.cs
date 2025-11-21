using UnityEngine;
using UnityEngine.SceneManagement;
public class Tripwire : MonoBehaviour
{
    public string sceneToLoad;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
        }
    }
}
