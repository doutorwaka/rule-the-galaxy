using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private List<GameObject> bulletsPrefab;
    private List<Bullet> bullets;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void Shoot(){
        if(this.bulletsPrefab != null){
            for(int i = 0; i < this.bulletsPrefab.Count; i++){
                Bullet b = bullets[i];
                b.IncreaseTimePast(Time.deltaTime);

                if(b.GetCooldown() < b.GetTimePast()){
                    b.ResetTimePast();                
                    GameObject bullet = Instantiate<GameObject>(this.bulletsPrefab[i]);
                    bullet.name = b.GetName();
                    bullet.transform.position = transform.position;
                    bullet.transform.rotation = transform.rotation;                    
                    
                    BulletMovement bmComp = bullet.AddComponent<BulletMovement>();
                    bmComp.SetMaxRange(b.GetMaxRange());
                    bmComp.SetSpeed(b.GetVelocity());
                    bmComp.setDirection(Vector3.down);

                    bullet.SetActive(true);
                }
            }
        }
    }

    public void SetBulletsPrefab(List<GameObject> bulletsPrefab){
        this.bulletsPrefab = bulletsPrefab;
    }

    public void SetBullets(List<Bullet> bullets){
        this.bullets = bullets;
    }
}
