using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
  private Animator anim;
  private Rigidbody2D rb;

  [SerializeField] private AudioSource deathSoundEffect;

  private void Start()
  {
    anim = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Trap"))
    {
      Die();
    }
  }

  private void Die()
  {
    deathSoundEffect.Play();
    rb.bodyType = RigidbodyType2D.Static;
    anim.SetTrigger("death");
  }

  private void RestartLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
