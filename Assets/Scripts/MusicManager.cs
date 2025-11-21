using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource currentSource;
    public AudioSource nextSource;
    public float fadeDuration = 2f;

    public AudioClip spaceMusic;
    public AudioClip earthMusic;
    public AudioClip finalMusic;
    public AudioClip endMusic;

    private static MusicManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Space"))
            ChangeMusic(spaceMusic);
        else if (scene.name.Contains("Earth"))
            ChangeMusic(earthMusic);
        else if (scene.name.Contains("Main"))
            ChangeMusic(finalMusic);
        else if (scene.name.Contains("End"))
            ChangeMusic(endMusic);
    }

    public void ChangeMusic(AudioClip newClip)
    {
        if (currentSource.clip == newClip)
            return;

        StopAllCoroutines();
        StartCoroutine(FadeToNewTrack(newClip));
    }

    private System.Collections.IEnumerator FadeToNewTrack(AudioClip newClip)
    {
        nextSource.clip = newClip;
        nextSource.volume = 0f;
        nextSource.Play();

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            currentSource.volume = Mathf.Lerp(1f, 0f, time / fadeDuration);
            nextSource.volume = Mathf.Lerp(0f, 1f, time / fadeDuration);
            yield return null;
        }

        currentSource.Stop();
        var temp = currentSource;
        currentSource = nextSource;
        nextSource = temp;
    }
}