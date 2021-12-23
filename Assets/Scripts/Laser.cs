using UnityEngine;

public class Laser : MonoBehaviour
{
    private int speed = 8;
    private GameObject _dodgeEnemy;

    private void Start()
    {
        

    }
    void Update()
    {
        CalculateMovement();
        DodgeEnemy();

    }

    void CalculateMovement()
    {

        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y >= 7)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    private void DodgeEnemy()
    {
            _dodgeEnemy = GameObject.Find("Dodge_Enemy");
            if (_dodgeEnemy != null)
            {
                _dodgeEnemy.GetComponent<DodgeEnemy>().PlayerLaserSpawned();
            }
    }

}
