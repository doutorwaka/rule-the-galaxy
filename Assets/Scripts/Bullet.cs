public class Bullet{

    private string name;
    private float velocity;
    private float turnRate;
    private float maxRange;
    private float cooldown;
    private float timePast;

    public Bullet(string name, float velocity, float turnRate, float maxRange, float cooldown){
        this.name = name;
        this.velocity = velocity;
        this.turnRate = turnRate;
        this.maxRange = maxRange;
        this.cooldown = cooldown;
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

    public void ResetTimePast(){
        this.timePast = 0f;
    }

    public void IncreaseTimePast(float time){
        this.timePast += time;
    }
}