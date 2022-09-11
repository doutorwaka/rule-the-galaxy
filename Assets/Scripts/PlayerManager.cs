using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] bulletsPrefab;
    private PlayerShip playerShip;
    private PlayerControls playerControls;
    private Dictionary<string, GameObject> bullets;
    // Start is called before the first frame update
    void Start()
    {        
        InitializePlayerShip();        
        playerControls = gameObject.GetComponentInChildren<PlayerControls>();
        InitializePlayerBullets();
    }

    // Update is called once per frame
    void Update()
    {
        // Manage the player movement
        playerControls.MovePlayer(playerShip.getSpeed(), playerShip.getRotationSpeed());
        
        // Manage the ship bullets
        List<Bullet> playerBullets = playerShip.getWeapons();
        playerControls.Shoot(playerShip.getBulletCooldown(), playerBullets, bullets);
    }

    private void InitializePlayerShip(){
        playerShip = new PlayerShip(270f, 5f, 1f, 3);
        playerShip.addWeapon(new Bullet("BulletPlayerLvl1", 10f, 0f, 10f));
    }

    private void InitializePlayerBullets(){
        bullets = new Dictionary<string, GameObject>();
        foreach(GameObject bullet in bulletsPrefab){
            bullet.SetActive(false);
            bullets.Add(bullet.name, bullet);
        }
    }
}
