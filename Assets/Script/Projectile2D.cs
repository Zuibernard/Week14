using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;   // จุดที่ยิงออก
    [SerializeField] private GameObject target;      // เป้าล็อค / Crosshair
    [SerializeField] private GameObject bulletPrefab;

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
                target.transform.position = hit.point;
                Debug.Log($"Hit {hit.collider.gameObject.name}");

                Vector2 projectileVelocity = CalculateProjectileVelocity(
                    shootPoint.position,
                    hit.point,
                    1f
                );

                GameObject bulletObj = Instantiate(
                    bulletPrefab,
                    shootPoint.position,
                    Quaternion.identity
                );

                Rigidbody2D shootBullet = bulletObj.GetComponent<Rigidbody2D>();

                if (shootBullet != null)
                {
                    shootBullet.linearVelocity = projectileVelocity;
                }
                else
                {
                    Debug.LogError("bulletPrefab ไม่มี Rigidbody2D!");
                }
            }
        }
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 direction = target - origin;

        return new Vector2(
            direction.x / time,
            (direction.y / time) + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time
        );
    }
}