using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField] private float xBound;
   
   private Transform _transform;

   private void Awake()
   {
      _transform = transform;
   }
   
   private void Update()
   {
      if (Input.GetKey(KeyCode.LeftArrow) && _transform.position.x > -xBound)
      {
         _transform.Translate(-0.1f, 0, 0);
      }
      if (Input.GetKey(KeyCode.RightArrow) && _transform.position.x < xBound)
      {
         _transform.Translate(0.1f, 0, 0);
      }
   }
}
