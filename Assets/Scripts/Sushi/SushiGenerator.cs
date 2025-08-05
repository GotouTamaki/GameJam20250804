using UnityEngine;

public class SushiGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] _generateTransforms;
    [SerializeField] private GameObject[] _generatePrefabs;
    [SerializeField] private int[] _generateWeights;
    [SerializeField] private float _initInterval = 0.5f;
    [SerializeField] private float _minLimmitInterval = 0.1f;

    private float _currentInterval = 0.5f;
    private float _timer = 0;

    void Start()
    {
        _timer = 0;
        _currentInterval = _initInterval;
    }

    void Update()
    {
        if (_timer > _currentInterval)
        {
            if (_generateTransforms.Length <= 0 || _generatePrefabs.Length <= 0 || _generateWeights.Length <= 0) return;

            Vector3 generatePosition = _generateTransforms[Random.Range(0, _generateTransforms.Length - 1)].position;
            int num = Choose(_generateWeights);
            Instantiate(_generatePrefabs[num], generatePosition, _generatePrefabs[num].transform.rotation);
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
