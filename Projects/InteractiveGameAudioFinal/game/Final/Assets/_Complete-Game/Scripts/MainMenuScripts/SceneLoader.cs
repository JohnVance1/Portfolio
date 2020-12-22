using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;
using FMODUnity;

public class SceneLoader : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string MenuClick;
    private EventInstance menuClick;

    [FMODUnity.EventRef]
    public string MainMusic;
    private EventInstance mainMusic;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        menuClick = FMODUnity.RuntimeManager.CreateInstance(MenuClick);
        mainMusic = FMODUnity.RuntimeManager.CreateInstance(MainMusic);
        mainMusic.start();
    }

    public void LoadScene(int level)
    {
        menuClick.start();
        mainMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene(level, LoadSceneMode.Single);

    }
}
