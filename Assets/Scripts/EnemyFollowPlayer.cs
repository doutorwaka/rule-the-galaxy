using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    private float velocity = 3f;
    private float turnRate = 30f;
    private float initialZ;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        initialZ = transform.position.z;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
            return;

        Vector3 playerDirection = CalculateRouteDirection();

        RotateEnemy(playerDirection);
        TranslateEnemy(playerDirection);
    }

    private Vector3 CalculateRouteDirection(){
        Vector3 playerDirection = player.transform.position - transform.position;
        playerDirection.Normalize();

        return playerDirection;
    }

    private void RotateEnemy(Vector3 direction){
        float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

        float rotationAmount = turnRate * Time.deltaTime;
        
        Quaternion desiredRot = Quaternion.Euler(0,0,zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, rotationAmount);
    }

    private void TranslateEnemy(Vector3 direction){
        float moveAmount = velocity * Time.deltaTime;
        Vector3 moveVector = Vector3.down * moveAmount;
        transform.Translate(moveVector);
    }

    public void SetVelocity(float velocity){
        this.velocity = velocity;
    }

    public void SetTurnRate(float turnRate){
        this.turnRate = turnRate;
    }
}
