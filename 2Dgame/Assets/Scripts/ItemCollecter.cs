using TMPro;
using UnityEngine;

public class ItemCollecter : MonoBehaviour
{
  private int cherries = 0;
  [SerializeField] private TMP_Text cherriesText;

  [SerializeField] private AudioSource collectionSoundEffect;
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Cherry"))
    {
      collectionSoundEffect.Play();
      Destroy(other.gameObject);
      cherries++;
      cherriesText.SetText("Cherries: " + cherries);
    }
  }
}
