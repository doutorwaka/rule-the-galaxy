using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] bulletsPrefab;
    private Dictionary<string, GameObject> bullets;
    private PlayerControls playerControls;
    private GameObject playerShipGo;
    private Ship playerShip;
    private string playerTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        PrepareAllBulletsPrefabs();
        InstantiateInitialShip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerShip(GameObject playerShipGo){
        this.playerShipGo = playerShipGo;
    }

    public void SetShip(Ship playerShip){
        this.playerShip = playerShip;
    }

    private void PrepareAllBulletsPrefabs(){
        this.bullets = new Dictionary<string, GameObject>();

        foreach(GameObject b in this.bulletsPrefab){
            bullets.Add(b.name, b);
        }
    }

    private void InstantiateInitialShip(){
        // Create the initial player ship characteristcs        
        playerShip = new Ship(1, 1, 10f, 120f, 1);        

        // Vinculate the player ship game object
        playerShipGo = GameObject.Find("PlayerShip");
        playerShipGo.transform.position = new Vector3(0f, 0f, -2f);

        // If we found the player ship game object,
        // vinculate the player controls with it.
        if(playerShipGo != null){
            playerShipGo.tag = this.playerTag;

            playerControls = playerShipGo.AddComponent<PlayerControls>();
            playerControls.SetShip(playerShip);

            // Create the initial bullet characteristcs
            Bullet initialBullet = new Bullet("BlueBeamLvl1", 20f, 0f, 20f, 1f, 5, 1);
            // Add the initial bullet characteristcs and prefab to the 
            // ship and player controls respectively
            AddBulletsToShip(initialBullet);
        }
    }

    private void AddBulletsToShip(Bullet b){
        playerControls.AddBullet(b, this.bullets[b.GetName()] );
    }
}
