using UnityEngine;

public class CoinsSpawn : MonoBehaviour
{
    public GameObject Template;
    public int _count;

    private void Start()
    {
        // создаем монетки
        for (int i = 0; i < _count; i++)
        {
            _ = Instantiate(Template, new Vector3(i + 3, 0, 0), Quaternion.identity);
        }

        for (int i = 0; i < _count; i++)
        {
            _ = Instantiate(Template, new Vector3(i - 6, 0, 0), Quaternion.identity);
        }
    }
}