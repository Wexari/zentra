using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    private Map currentMap;
    public bool freezeMenu;

    //public int curLevel { get; private set; }
    //public int maxLevel { get; private set; }

    public void Startup()
    {
        Debug.Log("Map manager starting...");


        //UpdateData(0, 3);

        status = ManagerStatus.Started;
    }

    public void LoadMap(string name)
    {
        Managers.actorManager.actor = FindObjectOfType<ActorController>();
        Managers.actorManager.actor.freeze = true;
        UnloadMap();
        Debug.Log("Loading " + name + "...");
        GameObject level = (GameObject)Resources.Load("Maps/" + name);
        currentMap = Instantiate(level).GetComponent<Map>();
        Managers.actorManager.actor.gameObject.transform.position = currentMap.GetSpawnPosition().position;
        Managers.actorManager.actor.gameObject.transform.rotation = currentMap.GetSpawnPosition().rotation;
        Managers.actorManager.actor.freeze = false;

        Managers.soundManager.PlayMusic(currentMap.music);
        Managers.soundManager.PlayAmbience(currentMap.ambience);
        freezeMenu = false;
    }

    public void LoadFreezeMap(string name)
    {
        Managers.actorManager.actor = FindObjectOfType<ActorController>();
        Managers.actorManager.actor.freeze = true;
        Managers.actorManager.actor.SwitchGun(false);
        UnloadMap();
        Debug.Log("Freeze loading " + name + "...");
        GameObject level = (GameObject)Resources.Load("Maps/" + name);
        currentMap = Instantiate(level).GetComponent<Map>();
        Managers.actorManager.actor.transform.position = currentMap.GetSpawnPosition().position;
        Managers.actorManager.actor.gameObject.transform.rotation = currentMap.GetSpawnPosition().rotation;

        Managers.soundManager.PlayMusic(currentMap.music);
        Managers.soundManager.PlayAmbience(currentMap.ambience);
        freezeMenu = true;
    }

    public void UnloadMap()
    {
        if(currentMap)
        {
            Destroy(currentMap.gameObject);
            Debug.Log("Map unloaded.");
        }
        else
        {
            Debug.Log("Can't unload map: there is no map.");
        }
    }

    //public void LoadMapManually(string name)
    //{
    //    SceneManager.LoadScene(name);
    //}

    //public void UpdateData(int curLevel, int maxLevel)
    //{
    //    this.curLevel = curLevel;
    //    this.maxLevel = maxLevel;
    //}

    //public void GoToNext()
    //{
    //    if(curLevel < maxLevel)
    //    {
    //        curLevel++;
    //        string name = "Level" + curLevel;
    //        Debug.Log("Loading " + name);
    //        SceneManager.LoadScene(name);
    //    }
    //    else
    //    {
    //        Debug.Log("Last level");
    //        Messenger.Broadcast(GameEvent.GAME_COMPLETE);
    //    }
    //}

    //public void ReachObjective()
    //{
    //    Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
    //}

    //public void RestartCurrent()
    //{
    //    string name = "Level" + curLevel;
    //    Debug.Log("Loading " + name);
    //    SceneManager.LoadScene(name);
    //}
}
