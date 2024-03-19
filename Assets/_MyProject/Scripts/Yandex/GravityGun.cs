using UnityEngine;

public class GravityGun : MonoBehaviour
{
    //скорость запуска объекта
    public float launchSpeed = 40;

    //ссылка на объект, который зафиксирован
    Rigidbody _target = null;
    Collider _collider = null;
    bool _isLocked = false;

    void Update()
    {
        //если пушка еще не зафиксировала объект
        if (!_isLocked)
        {
            //если нажата левая кнопка мыши
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                //пускаем луч через центр экрана
                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.6f, 0)), out hit))
                {
                    //если луч попал в объект с компонентом Rigidbody
                    if (hit.rigidbody != null)
                    {
                        //фиксируем объект
                        LockOnTarget(hit.rigidbody, hit.collider);
                    }
                }
            }
        }
        else
        {
            //если есть зафиксированный объект, перемещаем объект в точку пушки каждый кадр
            _target.transform.position = gameObject.transform.position;
            if (Input.GetMouseButtonDown(1))
            {
                //если нажата левая кнопка мыши, отпускаем объект
                ReleaseTarget();
            }
        }

    }

    void LockOnTarget(Rigidbody target, Collider collider)
    {
        //запоминаем какой объект зафиксирован
        _target = target;
        _collider = collider;

        _collider.enabled = !_collider.enabled;
        //отключаем физику у объекта
        _target.isKinematic = true;
        //перемещаем объект в точку пушки
        _target.transform.position = transform.position;
        _isLocked = true;
    }

    void ReleaseTarget()
    {
        _collider.enabled = !_collider.enabled;
        //включаем физику у объекта
        _target.isKinematic = false;
        //запускаем объект вперед со скоростью launchSpeed
        _target.velocity = transform.forward * launchSpeed;
        //обнуляем ссылку на объект
        _target = null;
        _isLocked = false;
    }
}
