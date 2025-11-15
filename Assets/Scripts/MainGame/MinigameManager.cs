using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public Transform popupParent;

    public GameObject[] zombies;

    public GameObject[] popups;
    public int currentPopup;

    public float range;
    public float timeBetweenZombies, timeBetweenWaves;
    public int zombiesInWave;


    private void Start()
    {
        Invoke("ZombieTick", 0);
        Invoke("SpawnWave", timeBetweenWaves);
    }

    private void SpawnZombie()
    {
        GameObject z = Instantiate(zombies[Random.Range(0, currentPopup)]);
        z.transform.position = Random.insideUnitCircle.normalized * range + (Vector2)transform.position;
    }

    private void ZombieTick()
    {
        SpawnZombie();
        Invoke("ZombieTick", timeBetweenZombies);
    }

    private void SpawnWave()
    {
        for (int i = 0; i < zombiesInWave; i++) { SpawnZombie(); }

        if (currentPopup < popups.Length && FindObjectOfType<Popup>() == null)
        {
            GameObject p = Instantiate(popups[currentPopup]);
            p.transform.parent = popupParent;
            p.transform.localPosition = Vector3.zero;
        }

        timeBetweenWaves *= 0.9f;
        Invoke("SpawnWave", timeBetweenWaves);
    }
}
