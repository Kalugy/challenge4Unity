using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    private float spawnRange = 9;
    public int enemyCount;

    public int waveNumber = 1;

    public GameObject[] powerupPrefab;

    public PlayerController playerControllerScript;

    public GameObject homingProjectilePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        int random = Random.Range(0, powerupPrefab.Length);
        Instantiate(powerupPrefab[random], GenerateSpawnPosition(), powerupPrefab[random].transform.rotation);
    }

    private void Update()
    {
        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;

        if (!playerControllerScript.gameOver)
        {
            if (enemyCount == 0)
            {
                waveNumber++;
                SpawnEnemyWave(waveNumber);
                int random = Random.Range(0, powerupPrefab.Length);
                Instantiate(powerupPrefab[random], GenerateSpawnPosition(), powerupPrefab[random].transform.rotation);
            }
        }
        else
        {
            HandleGameOver();
        }
        
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEne = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomEne], GenerateSpawnPosition(), enemyPrefab[randomEne].transform.rotation);

        }
    }

    public void FireProjectiles(GameObject player)
    {
        Debug.Log("FireProjectiles() called");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null || !enemy.activeInHierarchy) continue;

            Debug.Log("Generation");

            GameObject projectile = Instantiate(homingProjectilePrefab, player.transform.position, homingProjectilePrefab.transform.rotation);
            Debug.Log("Generation 22");

            HomingProjectile hp = projectile.GetComponent<HomingProjectile>();
            hp.target = enemy.transform;
        }

    }
    Transform GetFirstEnemy()
    {
        foreach (var enemy in enemyPrefab)
        {
            if (enemy != null)
            {
                return enemy.transform;
            }
        }

        return null; // No enemy found
    }




    // Update is called once per frame
    private Vector3 GenerateSpawnPosition()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnX, 0, spawnZ);
        return randomPos;
    }

    void HandleGameOver()
    {
        // Destroy all enemies
        foreach (Enemy enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
        {
            Destroy(enemy.gameObject);
        }

        // Destroy all powerups (assuming they have a Powerup script or tag)
        foreach (GameObject powerup in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            Destroy(powerup.gameObject);
        }

        foreach (GameObject powerup in GameObject.FindGameObjectsWithTag("ProjectilesPowerUp"))
        {
            Destroy(powerup.gameObject);
        }

    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Challenge 4");
    }

}
