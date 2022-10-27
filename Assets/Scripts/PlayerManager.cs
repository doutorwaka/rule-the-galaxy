using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] bulletsPrefab;
    private Dictionary<string, GameObject> bullets;
    private PlayerControls playerControls;
    private GameObject playerShipGo;
    private GameObject oldPlayerShipGo;
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
        Respawn();
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

        // If we found the player ship game object,
        // vinculate the player controls with it.
        if(playerShipGo != null){
            this.oldPlayerShipGo = Instantiate<GameObject>(playerShipGo);
            oldPlayerShipGo.SetActive(false);

            playerShipGo.transform.position = new Vector3(0f, 0f, -2f);
            
            playerShipGo.tag = this.playerTag;

            // Add player controls script
            playerControls = playerShipGo.AddComponent<PlayerControls>();
            playerControls.SetShip(playerShip);

            // Add do damage script to the player ship
            DoDamage doDamage = playerShipGo.AddComponent<DoDamage>();
            doDamage.SetIsImortal(false);
            doDamage.SetHpAmount(playerShip.GetHp());
            doDamage.SetDamageAmount(1);

            // Create the initial bullet characteristcs
            Bullet initialBullet = new Bullet("BlueBeamLvl1", 20f, 0f, 20f, 1f, 1, 1);
            // Add the initial bullet characteristcs and prefab to the 
            // ship and player controls respectively
            AddBulletsToShip(initialBullet);
        }
    }

    private void AddBulletsToShip(Bullet b){        
        playerControls.AddBullet(b, this.bullets[b.GetName()] );
    }

    private void Respawn(){
        GameObject playerShip = GameObject.FindGameObjectWithTag("Player");

        if(playerShip != null)
            return;

        if(Input.GetKeyUp(KeyCode.Space)){
            playerShip = Instantiate<GameObject>(oldPlayerShipGo);
            GameObject.Destroy(oldPlayerShipGo);
            playerShip.tag = playerTag;
            playerShip.name = "PlayerShip";
            playerShip.SetActive(true);
            InstantiateInitialShip();
        }
    }
}
