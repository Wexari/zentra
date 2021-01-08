using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupController : MonoBehaviour
{
    [SerializeField]
    private Slider progressBar;

    private void Awake()
    {
        Messenger<int, int>.AddListener(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
        Messenger.AddListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        Messenger<int, int>.RemoveListener(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
        Messenger.RemoveListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
    }

    private void OnManagersProgress(int numReady, int numModules)
    {
        float progress = (float)numReady / numModules;
        progressBar.value = progress;
    }

    private void OnManagersStarted()
    {
        SceneManager.LoadScene("Main");
        StartCoroutine(LoadEntryMapWithDelay(1f));
    }

    private IEnumerator LoadEntryMapWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Managers.mapManager.LoadFreezeMap("EntryMap");
        Destroy(gameObject);
    }
}