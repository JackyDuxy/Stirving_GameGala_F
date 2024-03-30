using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Gundata gunData;
    [SerializeField] private Transform muzzle;
    // [SerializeField] private GameObject ballPrefab; // Reference to the ball prefab

    float timeSinceLastShot;
    
    
    public void StartReload(){
        if (!gunData.is_reloading){
            StartCoroutine(Reload());
        }
    }
    private IEnumerator Reload(){
        gunData.is_reloading = true;
        yield return new WaitForSeconds(gunData.reload_time);
        gunData.ammo_current = gunData.magazine_size;
        gunData.is_reloading = false;
    }

    private void Start(){
        Player_shooting.shootInput += Shoot;
        Player_shooting.reloadInput += StartReload;

    }

    private bool CanShoot() => !gunData.is_reloading && timeSinceLastShot > 1f/(gunData.firerate/60f);

    public void Shoot(){
        if(gunData.ammo_current > 0 && Interact.shootable == true){
            if(CanShoot()){
                if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.max_distance)){
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                }
                }
        }
        else{
            // Pistol.SetActive(false); // Assuming the script is attached to the object you want to disappear
        } 
    }

    private void OnGunShot(){
        // Your logic for what happens when the gun is shot goes here
    }

    private void Update(){
        Debug.DrawRay(muzzle.position, muzzle.forward);
        timeSinceLastShot += Time.deltaTime;
    }
}
