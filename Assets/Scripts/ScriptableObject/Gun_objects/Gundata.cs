using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Gun", menuName="Weapon/Gun")]
public class Gundata : ScriptableObject
{
    // Gun name
    public new string name;
    //Shooting function
    public float damage;
    public float max_distance;

    // Reloading function
    public int ammo_current;
    public int magazine_size;
    public float firerate;
    public float reload_time;
    [HideInInspector]
    public bool is_reloading;

}
 
