using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float maxRange = 10f;
    private float speed = 10f;
    private float totalMoved = 0;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
        VerifyOutOfRange();
    }

    public void SetMaxRange(float maxRange){
        this.maxRange = maxRange;
    }

    public void SetSpeed(float speed){
        this.speed = speed;
    }

    private void MoveBullet(){
        float moveAmount = speed * Time.deltaTime;        
        totalMoved += moveAmount;
        Vector3 moveVector = this.direction * moveAmount;
        transform.Translate(moveVector);
    }

    private void VerifyOutOfRange(){
        if(totalMoved > maxRange){
            Destroy(gameObject);
        }
    }

    public void setDirection(Vector3 direction){
        this.direction = direction;
    }
}
