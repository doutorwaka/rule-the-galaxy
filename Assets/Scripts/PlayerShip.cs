using System;
using System.Collections.Generic;

public class PlayerShip
{
    private float rotationSpeed;
    private float speed;
    private int lvl;
    private float bulletCooldown;
    private int weaponMaxAmount;
    private List<Bullet> weapons;

    public PlayerShip(float rotationSpeed, float speed, float bulletCooldown, int weaponMaxAmount){
        this.rotationSpeed = rotationSpeed;
        this.speed = speed;
        this.lvl = 1;
        this.bulletCooldown = bulletCooldown;
        this.weaponMaxAmount = weaponMaxAmount;
        weapons = new List<Bullet>();
    }

    public void lvlUp(){
        this.lvl++;
        this.speed += 1;
        this.rotationSpeed += 0.5f;
        this.bulletCooldown -= 0.5f;
    }

    public float getRotationSpeed(){
        return rotationSpeed;
    }
    public float getSpeed(){
        return speed;
    }
    public int getLvl(){
        return lvl;
    }
    public float getBulletCooldown(){
        return bulletCooldown;
    }
    public int getWeaponMaxAmount(){
        return weaponMaxAmount;
    }
    public void addWeapon(Bullet weapon){
        this.weapons.Add(weapon);
    }
    public List<Bullet> getWeapons(){
        return weapons;
    }
}
