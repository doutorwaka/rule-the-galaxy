public class Bullet{

    private string name;
    private float velocity;
    private float turnRate;
    private float maxRange;
    private float cooldown;
    private float timePast;
    private int damage;
    private int hp;

    public Bullet(string name, float velocity, float turnRate, float maxRange, 
        float cooldown, int damage, int hp){

        this.name = name;
        this.velocity = velocity;
        this.turnRate = turnRate;
        this.maxRange = maxRange;
        this.cooldown = cooldown;
        this.damage = damage;
        this.hp = hp;
        this.timePast = 0;
    }

    public string GetName(){
        return name;
    }

    public float GetVelocity(){
        return velocity;
    }

    public float GetTurnRate(){
        return turnRate;
    }
    public float GetMaxRange(){
        return maxRange;
    }

    public float GetCooldown(){
        return this.cooldown;
    }

    public float GetTimePast(){
        return this.timePast;
    }

    public int GetDamage(){
        return this.damage;
    }

    public int GetHp(){
        return hp;
    }

    public int DecreaseHp(int amount){
        this.hp -= amount;
        return this.hp;
    }

    public void SetDamage(int damage){
        this.damage = damage;
    }

    public void ResetTimePast(){
        this.timePast = 0f;
    }

    public void IncreaseTimePast(float time){
        this.timePast += time;
    }    
}