using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "ScriptableObjects/Weapons")]
public class Weapon : ScriptableObject
{
    public GameObject bullet, pickup;
    public float firerate, damage;
    public Sprite sprite;
    public float spray;
}