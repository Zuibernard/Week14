using UnityEngine;
using UnityEngine.InputSystem; 

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;     
    [SerializeField] private GameObject target;         
    [SerializeField] private GameObject bulletPrefab;

    void Start()
    {

    }

    void Update()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
           
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 5f);

          
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            
            if (hit.collider != null)
            {
                target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log($"Hit {hit.collider.gameObject.name}");
            }
        }
    }
}