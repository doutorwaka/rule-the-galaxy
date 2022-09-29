using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] bulletsPrefabFromUnity;
    public GameObject[] enemyShipPrefabFromUnity;
    public float xMaxOffsetEnemySpawnPosition;
    public float yMaxOffsetEnemySpawnPosition;
    public float xMinOffsetEnemySpawnPosition;
    public float yMinOffsetEnemySpawnPosition;
    private Dictionary<string, GameObject>[] enemyShipsPrefabPerLvl;
    private Dictionary<string, Ship>[] enemyShipsDataPerLvl;
    private Dictionary<string, GameObject> bulletsPrefab;
    private GameObject playerShipGo = null;
    private float lastSpawntimePast = 0f;
    private float enemySpawnCooldown = 2f;
    private string enemyTag = "Enemy";
    
    // Start is called before the first frame update
    void Start()
    {
        PrepareAllEnemyShips();
        FindPlayerShip();
        PrepareAllBulletsPrefabs();
    }

    // Update is called once per frame
    void Update()
    {
        lastSpawntimePast += Time.deltaTime;
        FindPlayerShip();        
        SpawnEnemy();  
    }

    private void FindPlayerShip(){
        if(playerShipGo == null){
            playerShipGo = GameObject.Find("PlayerShip");
        }
    }

    private void SpawnEnemy(){
        // If we not found the player ship then
        // try again later
        if(playerShipGo == null || lastSpawntimePast < enemySpawnCooldown)
            return;

        // Otherwise, spawn an Random enemy
        // according the player ship lvl
        int lvl = 0;
        // Get the correct enemy prefab
        GameObject enemyPrefab = SelectEnemyShipPrefab(lvl);
        
        if(enemyPrefab == null){
            Debug.Log("O EnemyPrefab tá vindo nulo!");
            return;
        }
        // Calculate the new offset position for the new enemy
        // according with the player position
        Vector3 newEnemyPos = RandomNewEnemyPosition(playerShipGo.transform.position);
        // Calculate the new rotation
        // (should face the player ship?)
        Vector3 direction = CalculateRouteDirection(playerShipGo.transform.position, newEnemyPos);
        Quaternion newEnemyRot = RotateEnemy(direction);
        //Quaternion newEnemyRot = playerShipGo.transform.rotation;
        // Spawn the new enemy
        GameObject newEnemy = Instantiate(enemyPrefab, newEnemyPos, newEnemyRot);  
        newEnemy.name = enemyPrefab.name;      
        EnemyFollowPlayer followPlayerShip = newEnemy.AddComponent<EnemyFollowPlayer>();
        EnemyControl enemyControl = newEnemy.AddComponent<EnemyControl>();
        List<GameObject> bullets = SelectEnemyBulletsPrefab(lvl, enemyPrefab.name);
        List<Bullet> bulletsData = SelectEnemyShipData(lvl, newEnemy.name).GetBullets();
        enemyControl.SetBullets(bulletsData);
        enemyControl.SetBulletsPrefab(bullets);

        lastSpawntimePast = 0f;
    }

    private Ship SelectEnemyShipData(int lvl, string shipPrefabName){
        return this.enemyShipsDataPerLvl[lvl][shipPrefabName];
    }

    private List<GameObject> SelectEnemyBulletsPrefab(int lvl, string shipPrefabName){
        Ship enemyShip = SelectEnemyShipData(lvl, shipPrefabName);

        List<GameObject> bullets = new List<GameObject>();

        foreach(Bullet b in enemyShip.GetBullets()){
            bullets.Add(this.bulletsPrefab[b.GetName()]);
        }

        return bullets;
    }

    // Sort an enemy gameobject ship according to the lvl
    // (need improvements)
    private GameObject SelectEnemyShipPrefab(int lvl){
        Dictionary<string, GameObject>.KeyCollection keys = this.enemyShipsPrefabPerLvl[lvl].Keys;

        int nKeys = keys.Count;
        int sortedKey = Random.Range(0, nKeys);

        int i = 0;

        foreach(KeyValuePair<string, GameObject> pair in this.enemyShipsPrefabPerLvl[lvl]){
            if(i == sortedKey){
                return pair.Value;
            }
            i++;
        }

        return null;
    }

    private Vector3 RandomNewEnemyPosition(Vector3 initialPosition){
        int plusOrMinus = (Random.Range(0, 2) == 0) ? -1 : 1;

        float xNewPos = initialPosition.x +
                plusOrMinus * 
                Random.Range(xMinOffsetEnemySpawnPosition, xMaxOffsetEnemySpawnPosition);
        
        plusOrMinus = (Random.Range(0, 2) == 0) ? -1 : 1;

        float yNewPos = initialPosition.y + 
                plusOrMinus * 
                Random.Range(yMinOffsetEnemySpawnPosition, yMaxOffsetEnemySpawnPosition);

        return new Vector3(xNewPos, yNewPos, initialPosition.z);
    }

    private Vector3 CalculateRouteDirection(Vector3 playerShipPos, Vector3 enemyShipPos){
        Vector3 playerDirection = playerShipPos - enemyShipPos;
        playerDirection.Normalize();
        return playerDirection;
    }

    private Quaternion RotateEnemy(Vector3 direction){
        float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        return Quaternion.Euler(0,0,zAngle);
    }

    public void SetEnemySpawnCooldown(float cooldown){
        this.enemySpawnCooldown = cooldown;
    }

    private void PrepareAllBulletsPrefabs(){
        this.bulletsPrefab = new Dictionary<string, GameObject>();

        foreach(GameObject b in this.bulletsPrefabFromUnity){
            bulletsPrefab.Add(b.name, b);
        }
    }

    private void PrepareAllEnemyShips(){
        this.enemyShipsPrefabPerLvl = new Dictionary<string, GameObject>[this.enemyShipPrefabFromUnity.Length];
        this.enemyShipsDataPerLvl = new Dictionary<string, Ship>[this.enemyShipPrefabFromUnity.Length];

        for(int i = 0; i < this.enemyShipPrefabFromUnity.Length; i++){
            this.enemyShipsPrefabPerLvl[i] = new Dictionary<string, GameObject>();
            this.enemyShipsDataPerLvl[i] = new Dictionary<string, Ship>();

            for(int j = 0; j < enemyShipPrefabFromUnity[i].transform.childCount; j++){
                GameObject enemyShip = enemyShipPrefabFromUnity[i].transform.GetChild(j).gameObject;                
                enemyShip.tag = this.enemyTag;
                this.enemyShipsPrefabPerLvl[i].Add(enemyShip.name, enemyShip);

                Ship enemyShipData = new Ship(0, 2, 3f, 30f, 1);
                Bullet bullet = new Bullet("BulletEnemylvl1", 10f, 0f, 10f, 2f, 1, 1);
                enemyShipData.AddBullet(bullet);

                this.enemyShipsDataPerLvl[i].Add(enemyShip.name, enemyShipData);
            }
        }

        
    }
}
