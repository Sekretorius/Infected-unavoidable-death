using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Transform healthySpawn;
    [SerializeField] private Transform infectedSpawn;
    
    [SerializeField] private GameObject healthyRobot;
    [SerializeField] private GameObject infectedRobot;
    
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SpawnHealthy()
    {
        Instantiate(healthyRobot, healthySpawn.position, healthySpawn.rotation);
    }
    
    public void SpawnInfected()
    {
        Instantiate(infectedRobot, infectedSpawn.position, infectedSpawn.rotation);
    }
}
