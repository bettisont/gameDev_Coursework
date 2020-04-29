using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject completeLevelUI;
    public GameObject FPC;

    public void CompleteLevel()
    {

        completeLevelUI.SetActive(true);
        FPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.SetCursorLock(false);
    }
}
