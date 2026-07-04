using System.Collections;
using UnityEngine;

public class Girl : MonoBehaviour
{
    [Header("Head & Movement")]
    public Transform head;                   // الرأس الذي يتحرك
    private float headMoveInterval = 5f;     // كل كم ثانية تلف الرأس
    private int moveCount = 0;
    public bool isSinging = true;            // true = تغني، false = توقف

    [Header("Laser & Detection")]
    public GameObject laserPrefab;           // Prefab الليزر
    public float detectRange = 30f;          // نطاق كشف اللاعب
    public LayerMask playerLayer;            // Layer اللاعب
    public float laserCooldown = 1f;         // ثانية بين كل إطلاق
    private float lastLaserTime = 0f;

    public AudioSource GirlSound;
    public AudioSource robotSound;

    void Start()
    {
        StartCoroutine(HeadMovementRoutine());
    }

    void LateUpdate()
    {
        if (!isSinging)
            CheckPlayers();
    }

    // حركة الرأس (لف 180 درجة وارجاعه)
    IEnumerator HeadMovementRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(headMoveInterval);

            robotSound.Play();

            Quaternion startRot = head.localRotation;
            Quaternion backRot = startRot * Quaternion.Euler(0, 180f, 0);
            float t = 0f;
            while (t < 3f)
            {
                t += Time.deltaTime * 2f;
                head.localRotation = Quaternion.Slerp(startRot, backRot, t);
                yield return null;
            }
            yield return new WaitForSeconds(3f);

            isSinging = false;

            yield return new WaitForSeconds(8f);
            isSinging = true;
            yield return new WaitForSeconds(3f);

            t = 0f;
            while (t < 3f)
            {
                t += Time.deltaTime * 2f;
                head.localRotation = Quaternion.Slerp(backRot, startRot, t);
                yield return null;
            }

            moveCount++;
            if (moveCount % 2 == 0 && headMoveInterval > 3f)
                headMoveInterval -= 1f;

            GirlSound.Play();
        }
    }

    // كشف اللاعبين وإطلاق الليزر
    void CheckPlayers()
    {
        // فقط إذا انتهت فترة الـ Cooldown
        if (Time.time - lastLaserTime < laserCooldown)
            return;

        Collider[] players = Physics.OverlapSphere(transform.position, detectRange, playerLayer);

        foreach (Collider p in players)
        {
            Player playerScript = p.GetComponent<Player>();
            if (playerScript != null)
            {
                if (playerScript.currentMovementInput.x + playerScript.currentMovementInput.y == 0)
                {
                    return; // اللاعب لا يتحرك، لا تطلق الليزر
                }

                if (isSinging)
                {
                    return; // لا تطلق الليزر أثناء الغناء
                }

                ShootLaser(p.transform);
                lastLaserTime = Time.time; // إعادة ضبط الـ Cooldown
                break; // إطلاق ليزر واحد فقط لكل Cooldown
            }
        }
    }

    // إطلاق الليزر من الرأس نحو اللاعب
    void ShootLaser(Transform targetTransform)
    {
        GameObject laser = Instantiate(laserPrefab, head.position, Quaternion.identity);
        GirlLaser laserScript = laser.GetComponent<GirlLaser>();
        if (laserScript != null)
            laserScript.target = targetTransform; // تعيين الهدف
    }

    // رسم دائرة الرؤية في المشهد لتسهيل التصميم
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}