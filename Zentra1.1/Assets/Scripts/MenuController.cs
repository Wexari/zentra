using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private AudioClip clickSound;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !Managers.mapManager.freezeMenu)
        {
            Managers.actorManager.SwitchControl();
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        }
    }

    public void OnMasterVolumeChange(float value)
    {
        Managers.soundManager.ChangeMasterVolume(value);
    }

    public void OnMusicVolumeChange(float value)
    {
        Managers.soundManager.ChangeMusicVolume(value);
    }

    public void OnSFXVolumeChange(float value)
    {
        Managers.soundManager.ChangeSFXVolume(value);
    }

    public void OnAmbienceVolumeChange(float value)
    {
        Managers.soundManager.ChangeAmbienceVolume(value);
    }

    public void NewGame()
    {
        Managers.mapManager.LoadMap("L1");
        canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Test1()
    {
        Managers.mapManager.LoadMap("L2");
        canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Testing()
    {
        Managers.mapManager.LoadMap("Testing");
        canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PlayClickSound()
    {
        Managers.soundManager.PlayCloseSoundEffect(clickSound);
    }

    public void BackToMain()
    {
        Managers.mapManager.LoadFreezeMap("EntryMap");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}