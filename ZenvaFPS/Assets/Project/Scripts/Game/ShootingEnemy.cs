using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : Enemy
{
    public float shootingInterval = 4f;
    public float shootingDistance = 10f;
    public float chasingInterval = 2f;
    public float chasingDistance = 12f;
    public AudioSource audioSource;

    private GameManager gameManager;
    private Player player;
    private NavMeshAgent agent;
    private float shootingTimer;
    private float chasingTimer;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gameManager.AddEnemy(this);
        gameManager.UpdateEnemiesText();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(player.transform.position);
        shootingTimer = Random.Range(0, shootingInterval);
        chasingTimer = Random.Range(0, chasingInterval);
    }

    private void Update()
    {
        if (gameManager.gameOver)
        {
            agent.enabled = false;
            enabled = false;
            return;
        }
        ShootPlayer();
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        chasingTimer -= Time.deltaTime;
        if (chasingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= chasingDistance)
        {
            chasingTimer = chasingInterval;
            agent.SetDestination(player.transform.position);
        }
    }

    private void ShootPlayer()
    {
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= shootingDistance)
        {
            shootingTimer = shootingInterval;
            GameObject bulletObject = ObjectPoolManager.Instance.GetBullet();
            bulletObject.GetComponent<Bullet>().ShotByPlayer = false;
            bulletObject.transform.position = transform.position;
            bulletObject.transform.forward = (player.transform.position - transform.position).normalized;
        }
    }

    protected override void OnKill()
    {
        base.OnKill();
        audioSource.Play();
        gameManager.RemoveEnemy(this);
        gameManager.UpdateEnemiesText();
        agent.enabled = false;
        enabled = false;
        transform.localEulerAngles = new Vector3(10, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
