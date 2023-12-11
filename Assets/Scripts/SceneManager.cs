using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager _instance;

    private void Awake()
    {
        if( _instance == null || _instance !=this)
        {
            _instance = this;
        }
    }
    public void BackToMenu()
    {

    }
    public void ToMainGame()
    {

    }
}
