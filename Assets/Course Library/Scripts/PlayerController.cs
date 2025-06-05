using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool hasPowerUp;
    public bool projectilePowerUp = false;
    private float powerUpStrength = 15f;
    public GameObject powerUpIndicator;
    public bool gameOver = false;
    public Canvas Uicanvas;
    public SpawnManager spawnControllerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if(transform.position.y < -10)
        {
            gameOver = true;
            spawnControllerScript.waveNumber = 0;
            Uicanvas.gameObject.SetActive(true);
            transform.position =  new Vector3(0, 0, 0);
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }

        if (projectilePowerUp && hasPowerUp && Input.GetKeyDown(KeyCode.X))
        {
            spawnControllerScript.FireProjectiles(gameObject);
        }
    }

    public void Restart()
    {
        gameOver=false;
        transform.position = new Vector3(0, 0, 0);

        Uicanvas.gameObject.SetActive(false); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
        if (other.CompareTag("ProjectilesPowerUp"))
        {
            hasPowerUp = true;
            projectilePowerUp = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("collided with " + collision.gameObject.name + " with power up state: " + hasPowerUp);
            enemyRB.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }


    IEnumerator PowerupCountdownRoutine(){
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }
}
