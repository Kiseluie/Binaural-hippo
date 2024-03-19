using UnityEngine;

public class VacuumGrenadeLauncher : MonoBehaviour
{
    public VacuumGrenade GrenadePrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        VacuumGrenade Grenade = Instantiate(GrenadePrefab, transform.position, transform.rotation);
        Grenade.transform.LookAt(GetTargetPoint());
    }

    Vector3 GetTargetPoint()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return ray.GetPoint(100);
    }
}
