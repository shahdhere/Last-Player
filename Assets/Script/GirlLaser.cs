using UnityEngine;

public class GirlLaser : MonoBehaviour
{
    public float speed = 15f;        // سرعة الليزر
    public float lifeTime = 1f;      // كم يعيش قبل ما يختفي
    public Transform target;          // الهدف الذي يتوجه إليه الليزر

    void Start()
    {
        Destroy(gameObject, lifeTime); // يدمر نفسه بعد lifeTime
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 dir = (target.position - transform.position).normalized; // اتجاه اللاعب
            transform.position += dir * speed * Time.deltaTime;              // تحريك الليزر
            transform.rotation = Quaternion.LookRotation(dir);              // توجيه الليزر نحو الهدف
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Laser hit the player!");
            Player player = other.GetComponent<Player>();
            if (player != null)
                player.Die(); // استدعاء دالة الموت عند اللاعب

            Destroy(gameObject); // يحذف الليزر بعد أي تصادم
        }

    }
}