using UnityEngine;

public class InHaeReturn : MonoBehaviour
{
    private void OnDisable()
    {
        PoolManager.ReturnToPool(gameObject);
    }
}
