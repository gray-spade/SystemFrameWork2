using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : SingletonBehaviour<LobbyManager>
{
    public LobbyUIController LobbyUIController { get; private set; }

    protected override void Init()
    {
        m_IsDestroyOnLoad = true;
        base.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        LobbyUIController = FindObjectOfType<LobbyUIController>();

        if (!LobbyUIController) {
            Logger.Log("LobbyUIController dose not exist");
            return;
        }

        LobbyUIController.Init();
        UIManager.Instance.EnableStatsUI(true);
        AudioManager.Instance.PlayBGM(BGM.lobby);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
