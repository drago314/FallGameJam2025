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

    private int[] zombieStrengthShift = { 0, 0, 0, 5, 5, 5, 5};
    private int[] numZombiesPerWave = { 5, 10, 6, 20, 20, 20 };
    private int[] timeBetweenWaves = { 30, 15, 15, 20, 15, 15 };

    //private int zombieWindowLeft = 0;

    public float range;
    public float timeBetweenZombies;
    public int zombiesInWave;


    private void Start()
    {
        //zombiesInWave = 3;
        ////Invoke("ZombieTick", 20);
        ////Invoke("SpawnWave", 30);
    }

    public void StartGame()
    {
        zombiesInWave = 5;
        Invoke("ZombieTick", 0);
        Invoke("SpawnWave", 0);
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
        GameObject z = Instantiate(zombies[Mathf.Min(numWaves + zIndex, zombies.Length-1)]);
        z.transform.position = Random.insideUnitCircle.normalized * range + (Vector2)transform.position;
    }

    private void ZombieTick()
    {
        SpawnZombie();
        Invoke("ZombieTick", timeBetweenZombies);
    }

    private void SpawnWave()
    {
        zombiesInWave = numZombiesPerWave[Mathf.Min(numWaves, numZombiesPerWave.Length-1)];
        for (int i = 0; i < zombiesInWave; i++) { SpawnZombie(); }

        if (currentPopup < popups.Length && FindObjectOfType<Popup>() == null)
        {
            GameObject p = Instantiate(popups[currentPopup]);
            p.transform.parent = popupParent;
            p.transform.localPosition = Vector3.zero;
            float mult = Screen.width / 1920;
            p.transform.localScale = new Vector2(1, 1);
        }

        //timeBetweenWaves *= 0.9f;
        Invoke("SpawnWave", timeBetweenWaves[Mathf.Min(numWaves, timeBetweenWaves.Length-1)]);
        numWaves++;
        //Debug.Log(numWaves);
    }
}
