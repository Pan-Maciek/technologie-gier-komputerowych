using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera camera;
    public Rigidbody2D rb;
    public GameObject bulletPrefab;
    private float lastTime;
    public float BulletsPerSecond = 10;
    public float BulletVelocity = 2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time - lastTime > 1 / BulletsPerSecond) {
            Shoot();
            lastTime = Time.time;
        }
    }
    private void Shoot(){
        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        var lookDir = mousePos - rb.position;
        var angle = (Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg) * Mathf.Deg2Rad;

        var bullet = Instantiate(bulletPrefab, rb.position, Quaternion.Euler(0, 0, 0));
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * BulletVelocity, ForceMode2D.Impulse);
    }
}
