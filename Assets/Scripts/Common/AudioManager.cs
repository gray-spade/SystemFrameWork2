using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public enum BGM
{
    lobby,
    COUNT
}

public enum SFX
{
    chapter_clear,
    stage_clear,
    ui_button_click,
    COUNT
}



public class AudioManager : SingletonBehaviour<AudioManager>
{
    // 도 오브젝트에 변동할 변수
    public Transform BGMTransform;
    public Transform SFXTransform;

    // 오디오 파일 로드 경로
    const string AUDIO_PATH = "Audio";


    // 모든 BGM 오디오 리소스 저장할 컨테이너
    Dictionary<BGM, AudioSource> m_BGMPlayer = new Dictionary<BGM, AudioSource>();

    // 현재 재생하는 BGM  오디오소스 컴포넌트
    AudioSource m_CurrentBGMSource;
    // 모든 SFX 오디오 리소스 저장할 컨테이너
    Dictionary<SFX, AudioSource> m_SFXPlayer = new Dictionary<SFX, AudioSource>();

    protected override void Init()
    {
        base.Init();

        LoadBGMPlayer();
        LoadSFXPlayer();
    }


    // 존재하는 모든 BGM 파일 목록을 순회하면서 전용 게임오브젝트를 만들고
    // 그 오브젝트에 오디오 소스 컴포넌트를 붙여주고 해당 음원을 연동
    void LoadBGMPlayer()
    {
        for (int i = 0; i < (int)BGM.COUNT; i++)
        {
            var audioName = ((BGM)i).ToString();
            var pathStr = $"{AUDIO_PATH}/{audioName}";
            var audioClip = Resources.Load(pathStr, typeof(AudioClip)) as AudioClip;

            if (!audioClip)
            {
                Logger.LogError($"{audioName} clip does not exist.");
                continue;
            }

            var newGo = new GameObject(audioName);
            var newAudioSource = newGo.AddComponent<AudioSource>();
            newAudioSource.clip = audioClip;
            newAudioSource.loop = true;
            newAudioSource.playOnAwake = false;
            newGo.transform.parent = BGMTransform;

            m_BGMPlayer[(BGM)i] = newAudioSource;
        }
    }
    //Bgm과 같은 원리
    void LoadSFXPlayer()
    {
        for (int i = 0; i < (int)SFX.COUNT; i++)
        {
            var audioName = ((SFX)i).ToString();
            var pathStr = $"{AUDIO_PATH}/{audioName}";
            var audioClip = Resources.Load(pathStr, typeof(AudioClip)) as AudioClip;

            if (!audioClip)
            {
                Logger.LogError($"{audioName} clip does not exist.");
                continue;
            }

            var newGo = new GameObject(audioName);
            var newAudioSource = newGo.AddComponent<AudioSource>();
            newAudioSource.clip = audioClip;
            newAudioSource.loop = false;
            newAudioSource.playOnAwake = false;
            newGo.transform.parent = SFXTransform;

            m_SFXPlayer[(SFX)i] = newAudioSource;
        }
    }
    //BGM 플레이 함수
    public void PlayBGM(BGM bgm)
    {
        //만약 재생되고있는 BGM소스가 있다면
        //재생을 멈추고 null값으로 초기화
        if (m_CurrentBGMSource)
        {
            m_CurrentBGMSource.Stop();
            m_CurrentBGMSource = null;
        }
        //재생하고 싶은BGM이 존재하는지 확인
        //존재하지 않으면 에러를 발생
        if (m_BGMPlayer.ContainsKey(bgm) == false)
        {
            Logger.LogError($"Invalid clip name. {bgm}");
            return;
        }
        //존재한다면 해당 오디오소스 컴포넌트를 참조시키고
        //재생
        m_CurrentBGMSource = m_BGMPlayer[bgm];
        m_CurrentBGMSource.Play();
    }
    //BGM 일시정지
    public void PauseBGM()
    {
        if (m_CurrentBGMSource) m_CurrentBGMSource.Pause();
    }
    //BGM 재실행
    public void ResumeBGM()
    {
        if (m_CurrentBGMSource) m_CurrentBGMSource.UnPause();
    }
    //BGM 정지
    public void StopBGM()
    {
        if (m_CurrentBGMSource) m_CurrentBGMSource.Stop();
    }
    //SFX플레이 BGM이랑 같음
    public void PlaySFX(SFX sfx)
    {
        if (!m_SFXPlayer.ContainsKey(sfx))
        {
            Logger.LogError($"Invalid clip name. ({sfx})");
            return;
        }

        m_SFXPlayer[sfx].Play();
    }
    //음소거
    public void Mute()
    {
        foreach (var audioSourceItem in m_BGMPlayer)
        {
            audioSourceItem.Value.volume = 0f;
        }

        foreach (var audioSourceItem in m_SFXPlayer)
        {
            audioSourceItem.Value.volume = 0f;
        }
    }
    //음소거 해제
    public void UnMute()
    {
        foreach (var audioSourceItem in m_BGMPlayer)
        {
            audioSourceItem.Value.volume = 1f;
        }

        foreach (var audioSourceItem in m_SFXPlayer)
        {
            audioSourceItem.Value.volume = 1f;
        }
    }
}
