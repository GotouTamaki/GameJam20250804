using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    [System.Serializable]
    public class SushiPrefabData
    {
        [SerializeField] internal SushiType type;
        [SerializeField] internal SushiTouch prefab;
        [SerializeField] internal int initialPoolSize = 5;
    }

    [SerializeField] private SushiPrefabData[] sushiPrefabs;

    // 寿司の種類ごとのオブジェクトプール用Dictionary
    private Dictionary<SushiType, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<SushiType, Queue<GameObject>>();

        // 各寿司種類ごとに初期プールを作成
        foreach (var sushiData in sushiPrefabs)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < sushiData.initialPoolSize; i++)
            {
                GameObject obj = Instantiate(sushiData.prefab.gameObject);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(sushiData.type, objectPool);
        }
    }

    /// <summary>
    /// 寿司オブジェクトを取得
    /// </summary>
    public GameObject GetSushi(SushiType type, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogWarning($"寿司タイプ {type} がプールに登録されていません。");
            return null;
        }

        GameObject sushiObj;

        if (poolDictionary[type].Count > 0)
        {
            sushiObj = poolDictionary[type].Dequeue();
        }
        else
        {
            // 足りない場合は新しく生成
            var prefabData = System.Array.Find(sushiPrefabs, d => d.type == type);
            sushiObj = Instantiate(prefabData.prefab.gameObject);
        }

        sushiObj.transform.SetPositionAndRotation(position, rotation);
        sushiObj.SetActive(true);
        return sushiObj;
    }

    /// <summary>
    /// 寿司オブジェクトを返却
    /// </summary>
    public void ReturnSushi(SushiType type, GameObject sushiObj)
    {
        sushiObj.SetActive(false);

        if (poolDictionary.ContainsKey(type))
        {
            poolDictionary[type].Enqueue(sushiObj);
        }
        else
        {
            Destroy(sushiObj);
        }
    }
}
