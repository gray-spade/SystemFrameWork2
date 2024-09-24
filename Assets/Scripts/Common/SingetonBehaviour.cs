using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingetonBehaviour<T> : MonoBehaviour where T :SingetonBehaviour<T>
{
    protected bool m_IsDestroyOnLoad = false;//씬전환시 삭제 여부

    protected static T m_Instamce;

    public static T Instance {
        get { return m_Instamce; }
    
    }
    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        if (m_Instamce == null)
        {
            m_Instamce = (T)this;

            if (!m_IsDestroyOnLoad)
            {
                DontDestroyOnLoad(this);

            }
        }
        else {

            Destroy(this);
        }
    }

    protected virtual void OnDestroy() {
        Dispose();
    
    }

    protected virtual void Dispose()
    {
        m_Instamce = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
