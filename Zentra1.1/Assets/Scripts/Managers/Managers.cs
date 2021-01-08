using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MapManager))]
[RequireComponent(typeof(ActorManager))]
[RequireComponent(typeof(SoundManager))]

public class Managers : MonoBehaviour
{
    public static MapManager mapManager { private set; get; }
    public static ActorManager actorManager { private set; get; }
    public static SoundManager soundManager { private set; get; }

    private List<IGameManager> _startSequence;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        mapManager = GetComponent<MapManager>();
        actorManager = GetComponent<ActorManager>();
        soundManager = GetComponent<SoundManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(mapManager);
        _startSequence.Add(actorManager);
        _startSequence.Add(soundManager);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in _startSequence)
        {
            manager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }

            if (numReady > lastReady)
            {
                Debug.Log("Progress: " + numReady + "/" + numModules);
                Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            }
            yield return null;
        }

        Debug.Log("All managers started up");
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
    }
}
