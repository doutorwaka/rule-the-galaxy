using UnityEngine;

public class DoDamage : MonoBehaviour
{
    private int damageAmount = 0;
    private int hp;
    private bool isImortal = false;

    private float delayDefault = 0.03f;

    private string playerTag = "Player";
    private string enemyTag = "Enemy";
    private string playerBulletTag = "PlayerBullet";
    private string enemyBulletTag = "EnemyBullet";    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDamageAmount(int damageAmount){
        this.damageAmount = damageAmount;
    }

    public void SetHpAmount(int hp){
        this.hp = hp;
    }

    public void TakeDamage(int damageAmount, float delayToDestroy){
        if(!isImortal){
            this.hp -= damageAmount;

            if(this.hp <= 0){
                DestroyMyself(delayToDestroy);
            }
        }
    }

    public void SetIsImortal(bool isImortal){
        this.isImortal = isImortal;
    }

    private void DestroyMyself(float delay){
        GameObject.Destroy(gameObject, delay);
    }

    void OnTriggerEnter2D(Collider2D other){

        // Prevent friend fire (need improvement)
        if((this.gameObject.tag == playerTag && other.tag == playerBulletTag) || 
            (this.gameObject.tag == playerBulletTag && other.tag == playerTag) ||
            (this.gameObject.tag == enemyTag && other.tag == enemyBulletTag) || 
            (this.gameObject.tag == enemyBulletTag && other.tag == enemyTag))
            return;

        if(gameObject.tag != other.gameObject.tag){
            
            DoDamage otherDamage = other.gameObject.GetComponent<DoDamage>();

            // If otherDamage was found, we are 
            // able to do damage.
            if(otherDamage != null){
                otherDamage.TakeDamage(this.damageAmount, delayDefault);
            }
            
            Debug.Log("Eu sou o " + gameObject.name + " e causo " 
                + this.damageAmount + " de dano no " 
                + other.gameObject.name + ".");
        }
    }
}
