using UnityEngine;

public class EnemyFov : MonoBehaviour
{
    [SerializeField]
    private Transform fovPrefab;

    public FieldOfView fov { get; private set; }

    private void OnEnable()
    {
        GetComponent<Enemy>().enemyHasDied += DestroyFov;
    }

    private void OnDisable()
    {
        GetComponent<Enemy>().enemyHasDied -= DestroyFov;
    }

    private void Start()
    {
        fov = Instantiate(fovPrefab, null).GetComponent<FieldOfView>();
        fov.rayCount = 5;
    }

    private void Update()
    {
        if(!GameMaster.gameIsPaused)
        {
            fov.SetOrigin(transform.position);
            fov.SetAimDirection(transform.up);
        }
    }

    private void DestroyFov()
    {
        Destroy(fov.gameObject);
    }

}
