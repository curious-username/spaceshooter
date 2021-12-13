using UnityEngine;

public class TempMissile : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyLocations;
    private GameObject _closestEnemy;
    private float _enemyDistance = 1000f;
    private float _speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {


        _enemyLocations = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in _enemyLocations)
        {
            float distance = Vector2.Distance(enemy.transform.position, transform.position);
            if (distance <= _enemyDistance)
            {
                _closestEnemy = enemy;
                _enemyDistance = distance;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
        Vector3 direction = _closestEnemy.transform.position - transform.position;
        direction.Normalize();
        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);


    }





}
