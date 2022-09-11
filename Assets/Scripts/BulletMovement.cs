using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float maxRange = 10f;
    private float speed = 10f;
    private float totalMoved = 0;
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

    public void setMaxRange(float maxRange){
        this.maxRange = maxRange;
    }

    public void setSpeed(float speed){
        this.speed = speed;
    }

    private void MoveBullet(){
        float moveAmount = speed * Time.deltaTime;        
        totalMoved += moveAmount;
        Vector3 moveVector = Vector3.up * moveAmount;
        transform.Translate(moveVector);
    }

    private void VerifyOutOfRange(){
        if(totalMoved > maxRange){
            Destroy(gameObject);
        }
    }
}
