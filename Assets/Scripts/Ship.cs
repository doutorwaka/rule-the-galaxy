using System.Collections.Generic;

/**
 * Class that describes the attributes of one ship.
 */
public class Ship{
    // Player lvl
    private int lvl;
    // Ship hp
    private int hp;
    // Ship velocity to move forward
    private float velocity;
    // Ship turn rate
    private float turnRate;
    // Amount of weapons
    private int nBullet;
    // List of all bullets
    private List<Bullet> bullets;

    public Ship(int initialLvl, int initialHp, float initialVelocity, 
        float initialTurnRate, int nMaxBullet){

        this.lvl = initialLvl;
        this.hp = initialHp;
        this.velocity = initialVelocity;
        this.turnRate = initialTurnRate;
        this.nBullet = nMaxBullet;

        this.bullets = new List<Bullet>();
    }

    public void AddBullet(Bullet bullet){
        this.bullets.Add(bullet);
    }

    /**
     * Method to be called when the player 
     * level up. It will do all the rules
     * to level up.
     */
    public void LvlUp(){
        this.lvl++;
    }

    public int GetLvl(){
        return this.lvl;
    }

    public int GetHp(){
        return this.hp;
    }

    public float GetVelocity(){
        return this.velocity;
    }

    public float GetTurnRate(){
        return this.turnRate;
    }

    public int GetNBullets(){
        return this.nBullet;
    }

    public List<Bullet> GetBullets(){
        return this.bullets;
    }
}