using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRoomEveryNSecondsMono : MonoBehaviour
{

    public GameObject[] m_roomInScene;
    public float m_switchRoomEveryNSeconds = 30.0f;
    private int m_currentRoomIndex = 0;


    public bool m_useAutoSwitch = true;

    private void OnEnable()
    {
        if(m_useAutoSwitch)
            StartCoroutine(CoroutineSwitch());
    }


    [ContextMenu("Next")]
    public void Next()
    {
        m_currentRoomIndex = (m_currentRoomIndex + 1) % m_roomInScene.Length;
        for (int i = 0; i < m_roomInScene.Length; i++)
        {
            m_roomInScene[i].SetActive(false);
        }
        m_roomInScene[m_currentRoomIndex].SetActive(true);
    }

    [ContextMenu("Next")]
    public void Previous()
    {
        m_currentRoomIndex = (m_currentRoomIndex - 1 + m_roomInScene.Length) % m_roomInScene.Length;
        for (int i = 0; i < m_roomInScene.Length; i++)
        {
            m_roomInScene[i].SetActive(false);
        }
        m_roomInScene[m_currentRoomIndex].SetActive(true);
    }


    public IEnumerator CoroutineSwitch() { 
    
        while (true)
        {
            Next();
            yield return new WaitForSeconds(m_switchRoomEveryNSeconds);
            yield return new WaitForEndOfFrame();
        }
    }

}
