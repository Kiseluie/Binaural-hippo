using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VacuumGrenade : MonoBehaviour
{
    //сфера взрыва
    public Transform ExplosionSphere;
    //модель гранаты
    public GameObject GrenadeModel;
    //таймер взрыва
    public float ExplosionTimer = 3;
    //время притягивания врагов
    public float pullTime = 0.5f;
    //радиус взрыва
    public float radius = 5;
    //скорость полета гранаты
    public float LaunchSpeed = 10;

    bool _isExploded = false;
    List<EnemyHealth> _enemies = new List<EnemyHealth>();

    void Start()
    {
        //задаем таймер взрыва
        Invoke("Explosion", 4);
        //запускаем гранату вперед со скоростью LaunchSpeed
        GetComponent<Rigidbody>().velocity = transform.forward * LaunchSpeed;
    }

    //метод взрыва
    void Explosion()
    {
        //получаем все коллайдеры в радиусе взрыва
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        //перебираем все коллайдеры в цикле
        for (int i = 0; i < colliders.Length; i++)
        {
            //если коллайдер содержит компонент EnemyHealth
            EnemyHealth enemyHealth = colliders[i].GetComponent <EnemyHealth> ();
            if (enemyHealth != null)
            {
                //добавляем врага в список
                _enemies.Add(enemyHealth);
                //отключаем компонент навигации и скрипт врага
                enemyHealth.GetComponent <NavMeshAgent> ().enabled = false;
                enemyHealth.GetComponent <EnemyAI> ().enabled = false;
            }
        }
        //включаем флаг взрыва
        _isExploded = true;
        //устанавливаем размер сферы взрыва
        ExplosionSphere.localScale = Vector3.one * radius * 2;
        //включаем сферу взрыва
        ExplosionSphere.gameObject.SetActive(true);
        //отключаем физику у гранаты
        GetComponent<Rigidbody>().isKinematic = true;
        //отключаем модель гранаты
        GrenadeModel.SetActive(false);
    }

    void Update()
    {
        //если граната взорвалась
        if (_isExploded)
        {
            //уменьшаем таймер притягивания
            pullTime -= Time.deltaTime;
            //перебираем всех врагов в списке
            for (int i = 0; i < _enemies.Count; i++)
            {
                //если враг еще не уничтожен
                if (_enemies[i] != null)
                {
                    //плавно перемещаем врага к центру взрыва
                    _enemies[i].transform.position = Vector3.Lerp(_enemies[i].transform.position, transform.position, Time.deltaTime * 3);
                    //плавно уменьшаем размер врага
                    _enemies[i].transform.localScale = Vector3.Lerp(_enemies[i].transform.localScale, Vector3.zero, Time.deltaTime * 3);
                }
            }
            //если таймер притягивания истек
            if (pullTime <= 0)
            {
                //уничтожаем всех врагов
                DestroyAllEnemies();
                //уничтожаем гранату
                Destroy(gameObject);
            }
        }
    }

    //уничтожаем всех врагов попавших в радиус взрыва
    void DestroyAllEnemies()
    {
        //перебираем всех врагов в списке
        for (int i = 0; i < _enemies.Count; i++)
        {
            //уничтожаем i-ого врага
            Destroy(_enemies[i]);
        }
    }
}
