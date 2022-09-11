using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private GameObject playerShip;
    private float timePastSinceLastShoot = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerShip = GameObject.Find("PlayerShip");
    }

    // Update is called once per frame
    void Update()
    {
        timePastSinceLastShoot += Time.deltaTime;
    }

    public void MovePlayer(float playerShipSpeed, float playerShipRotationSpeed){
        RotatePlayer(playerShipRotationSpeed);
        TranslatePlayer(playerShipSpeed);
    }

    public void Shoot(float cooldown, List<Bullet> playerBullets, Dictionary<string, GameObject> bulletsPrefab){
        if(Input.GetButton("Jump") && timePastSinceLastShoot >= cooldown){

            foreach(Bullet b in playerBullets){

                GameObject bullet = Instantiate(bulletsPrefab[b.getName()], 
                    playerShip.transform.position, playerShip.transform.rotation);

                BulletMovement bm = bullet.AddComponent<BulletMovement>();
                bm.setMaxRange(b.getMaxRange());
                bm.setSpeed(b.getVelocity());

                bullet.SetActive(true);
            }

            timePastSinceLastShoot = 0;
        }
    }

    private void RotatePlayer(float playerShipRotationSpeed){
        float rotateAngle = playerShipRotationSpeed * Time.deltaTime * (-Input.GetAxis("Horizontal"));
        playerShip.transform.Rotate(Vector3.forward, rotateAngle);
    }

    private void TranslatePlayer(float playerShipSpeed){
        float moveAmount = playerShipSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        Vector3 moveVector = Vector3.up * moveAmount;
        playerShip.transform.Translate(moveVector);
    }
}
