using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public Transform popupParent;

    public GameObject[] zombies;

    public GameObject[] popups;
    public int currentPopup;

    private int numWaves = 0;

    private int[] numZombiesPerWave = { 5, 20, 20, 20 };
    private int[] timeBetweenWaves = { 30, 15, 15, 15 };

    //private int zombieWindowLeft = 0;

    public float range;
    public float timeBetweenZombies;
    public int zombiesInWave;


    private void Start()
    {
        zombiesInWave = 3;
        Invoke("ZombieTick", 0);
        Invoke("SpawnWave", 30);
    }

    private void SpawnZombie()
    {
        float r = Random.Range(0, 100);
        int zIndex = 0;
        if (r < 40)
        {
            zIndex = 0;
        }
        else if (r < 65)
        {
            zIndex = 1;
        }
        else if (r < 80)
        {
            zIndex = 2;
        }
        else if (r < 93)
        {
            zIndex = 3;
        }
        else
        {
            zIndex = 4;
        }
        GameObject z = Instantiate(zombies[numWaves + zIndex]);
        z.transform.position = Random.insideUnitCircle.normalized * range + (Vector2)transform.position;
    }

    private void ZombieTick()
    {
        SpawnZombie();
        Invoke("ZombieTick", timeBetweenZombies);
    }

    private void SpawnWave()
    {
        zombiesInWave = numZombiesPerWave[numWaves];
        for (int i = 0; i < zombiesInWave; i++) { SpawnZombie(); }

        if (currentPopup < popups.Length && FindObjectOfType<Popup>() == null)
        {
            GameObject p = Instantiate(popups[currentPopup]);
            p.transform.parent = popupParent;
            p.transform.localPosition = Vector3.zero;
        }

        //timeBetweenWaves *= 0.9f;
        Invoke("SpawnWave", timeBetweenWaves[numWaves]);
        numWaves++;
        Debug.Log("new wave");
    }
}
