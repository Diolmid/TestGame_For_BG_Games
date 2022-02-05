using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public bool shieldIsActive;
   
   public GameObject victoryParticle;
   public GameObject dieParticle;
   
   public Color normalColor;
   public Color underShieldColor;

   private Renderer _renderer;
   private Vector3 _startPosition;

   private void Awake()
   {
      _renderer = GetComponent<Renderer>();
      _startPosition = transform.position;
   }

   private void Update()
   {
      _renderer.material.color = shieldIsActive ? underShieldColor : normalColor;
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("End"))
      {
         StartCoroutine(Victory());
      }

      if (other.CompareTag("DeadZone") && !shieldIsActive)
      {
         Die();
      }
   }

   private void Die()
   {
      Destroy(Instantiate(dieParticle, transform.position, Quaternion.identity), 3);
      GameManager.instance.SpawnNewPlayer(_startPosition);
      gameObject.SetActive(false);
   }
   
   private IEnumerator Victory()
   {
      Destroy(Instantiate(victoryParticle, transform.position,Quaternion.identity), 3);
      yield return new WaitForSeconds(3);
      GameManager.instance.ReloadCurrentScene();
   }
}
