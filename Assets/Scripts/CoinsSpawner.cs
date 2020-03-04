using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    public GameObject Template;
    public int _count;

    private void Start()
    {
        for (int i = 0; i < _count; i++)
        {
            GameObject coin = Instantiate(Template, new Vector3(0, 0, 0), Quaternion.identity);
            Transform coinTransform = coin.GetComponent<Transform>();
            coinTransform.position = new Vector3(3 + i, 0, 0);
        }
    }
}