using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool devilFinish = false;
    public bool angleFinish = false;

    private void Start()
    {
        devilFinish = angleFinish = false;
    }

    private void FixedUpdate()
    {
        if (angleFinish && devilFinish)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void devilDead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void devilFin()
    {
        devilFinish = true;
    }
    public void angleFin()
    {
        angleFinish = true;
    }
}
