using System.Collections.Generic;
using UnityEngine;

public class SushiGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] _leftGenerateTransforms;
    [SerializeField] private Transform[] _rightGenerateTransforms;
    [SerializeField] private SushiTouch[] _generatePrefabs;
    [SerializeField] private int[] _generateWeights;
    [SerializeField] private float _initInterval = 0.5f;
    [SerializeField] private float _minLimmitInterval = 0.1f;

    private float _currentInterval = 0.5f;
    private float _timer = 0;

    // 種類ごとのプールを Dictionary で管理
    private Dictionary<SushiType, ObjectPool<SushiTouch>> _sushiPools;

    public Dictionary<SushiType, ObjectPool<SushiTouch>> GetObjectPools => _sushiPools;

    void Start()
    {
        _sushiPools = new Dictionary<SushiType, ObjectPool<SushiTouch>>();

        foreach (var prefab in _generatePrefabs)
        {
            SushiType type = prefab.SushiParameter.Type; // ← SushiMove 内で SushiParameter を持っている想定

            _sushiPools[type] = new ObjectPool<SushiTouch>(
                createFunc: () =>
                {
                    SushiTouch obj = Instantiate(prefab);
                    obj.gameObject.SetActive(false);
                    return obj;
                },
                onGet: (obj) => obj.gameObject.SetActive(true),
                onRelease: (obj) => obj.gameObject.SetActive(false),
                initialSize: 5
            );
        }
    }

    void Update()
    {
        if (_timer > _currentInterval)
        {
            if (_leftGenerateTransforms.Length <= 0 || _generatePrefabs.Length <= 0 || _generateWeights.Length <= 0) return;

            Vector3 generatePosition = Vector3.zero;
            int randamLR = Random.Range(0, 2); // 0: left, 1: right

            if (randamLR == 0)
            {
                generatePosition = _leftGenerateTransforms[Random.Range(0, _leftGenerateTransforms.Length)].position;
            }
            else
            {
                generatePosition = _rightGenerateTransforms[Random.Range(0, _rightGenerateTransforms.Length)].position;
            }

            int num = Choose(_generateWeights);
            SushiTouch sushiTouch = _sushiPools[(SushiType)num].Get();
            SushiMove sushiMove = sushiTouch.SushiMove;

            sushiMove.gameObject.transform.SetPositionAndRotation(generatePosition, _generatePrefabs[num].transform.rotation);
            sushiMove.SetDirection(randamLR == 0 ? MoveDirectionType.Right : MoveDirectionType.Left);

            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    /// <summary>抽選メソッド</summary>
    public int Choose(int[] weight)
    {
        float total = 0f;

        //配列の要素をtotalに代入
        for (int i = 0; i < weight.Length; i++)
        {
            total += weight[i];
        }

        //Random.valueは0.1から1までの値を返す
        float random = Random.value * total;

        //weightがrandomより大きいかを探す
        for (int i = 0; i < weight.Length; i++)
        {
            if (random < weight[i])
            {
                //ランダムの値より重みが大きかったらその値を返す
                return i;
            }
            else
            {
                //次のweightが処理されるようにする
                random -= weight[i];
            }
        }

        //なかったら最後の値を返す
        return weight.Length - 1;
    }
}
