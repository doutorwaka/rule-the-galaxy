public class Bullet{

    private string name;
    private float velocity;
    private float turnRate;
    private float maxRange;

    public Bullet(string name, float velocity, float turnRate, float maxRange){
        this.name = name;
        this.velocity = velocity;
        this.turnRate = turnRate;
        this.maxRange = maxRange;
    }

    public string getName(){
        return name;
    }

    public float getVelocity(){
        return velocity;
    }

    public float getTurnRate(){
        return turnRate;
    }
    public float getMaxRange(){
        return maxRange;
    }
}