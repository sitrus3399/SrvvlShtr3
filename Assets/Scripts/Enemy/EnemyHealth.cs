using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        //Mendapatkan reference komponen
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }

    void Update ()
    {
        //Check jika sinking
        if (isSinking)
        {
            //Memindahkan object kebawah
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        //Check jika dead
        if (isDead)
            return;

        //Play audio
        enemyAudio.Play ();

        //Kurangi health
        currentHealth -= amount;

        //Ganti posisi particle
        hitParticles.transform.position = hitPoint;

        //Play particle system
        hitParticles.Play();

        //Dead jika health kurang dari / sama dengan 0
        if (currentHealth <= 0)
        {
            Death ();
        }
    }

    void Death ()
    {
        //Set isDead
        isDead = true;

        //SetCapsulecollider ke trigger
        capsuleCollider.isTrigger = true;

        //Trigger play animation Dead
        anim.SetTrigger ("Dead");

        //Play Sound Dead
        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        //Disable Navmesh component
        GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
        //Set rigidbody ke kinematic
        GetComponent<Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
