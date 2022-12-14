using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private GameObject playerShip = null;
    private Ship ship = null;
    private List<GameObject> bulletsPrefab;
    private string playerBulletTag = "PlayerBullet";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Try find the player ship
        playerShip = GameObject.FindGameObjectWithTag("Player");

        // If the player ship was not found, return later
        if(playerShip == null || this.ship == null)
            return;

        // Otherwise, move the ship
        MoveShip();
        Shoot();        
    }

    private void Shoot(){
        for(int i = 0; i < this.bulletsPrefab.Count; i++){
            Bullet b = this.ship.GetBullets()[i];
            b.IncreaseTimePast(Time.deltaTime);

            if(b.GetCooldown() < b.GetTimePast()){
                b.ResetTimePast();                
                GameObject bullet = Instantiate<GameObject>(this.bulletsPrefab[i]);
                bullet.name = b.GetName();
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.tag = playerBulletTag;

                // Add bullet movement script to the bullet prefab
                BulletMovement bmComp = bullet.AddComponent<BulletMovement>();
                bmComp.SetMaxRange(b.GetMaxRange());
                bmComp.SetSpeed(b.GetVelocity());
                bmComp.setDirection(Vector3.up);

                // Add DoDamage script to the bullet prefab
                DoDamage bulletDamage = bullet.AddComponent<DoDamage>();
                bulletDamage.SetDamageAmount(b.GetDamage());
                bulletDamage.SetIsImortal(false);
                bulletDamage.SetHpAmount(b.GetHp());

                bullet.SetActive(true);
            }
        }
    }

    private void MoveShip(){
        RotateShip();
        TranslateShip();
    }

    private void RotateShip(){
        // Calculate the rotation amount based on the time past since the last
        // update, the ship turn rate ratio and the button pressed by the player.
        float rotateAmount = Time.deltaTime * this.ship.GetTurnRate() * Input.GetAxis("Horizontal");
        // Calculate the amount based on the -Z axis
        Vector3 rotate = Vector3.back * rotateAmount;
        // Do the rotate
        transform.Rotate(rotate);
    }

    private void TranslateShip(){
        // Calculate the amount of movement based on the time past since the last
        // update, the ship velocity ratio and the button pressed by the player
        float moveAmount = Time.deltaTime * this.ship.GetVelocity() * Input.GetAxis("Vertical");
        // The movement is calculated based on the Y axis
        Vector3 movement = Vector3.up * moveAmount;
        // Do the movement
        transform.Translate(movement);
    }

    // This method should be called when an instance
    // of PlayerControls is created. Set the player ship
    // characteristcs.
    public void SetShip(Ship ship){
        this.ship = ship;
    }

    // Whenever a bullet should be add to the ship, we must use
    // this method.
    public void AddBullet(Bullet bulletData, GameObject bulletPrefab){
        // Add the bullet data to the ship list
        this.ship.AddBullet(bulletData);
        // Add the bullet prefab to the ship bullet prefab list
        AddBulletPrefab(bulletPrefab);
    }

    public Ship GetShip(){
        return this.ship;
    }

    public void ClearBulletPrefab(){
        this.bulletsPrefab.Clear();
    }

    private void AddBulletPrefab(GameObject bulletPrefab){
        if(this.bulletsPrefab == null)
            this.bulletsPrefab = new List<GameObject>();

        this.bulletsPrefab.Add(bulletPrefab);
    }
}
