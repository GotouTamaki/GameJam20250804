using System.Collections.Generic;
using UnityEngine;

public class SushiGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] _leftGenerateTransforms;
    [SerializeField] private Transform[] _rightGenerateTransforms;
    [SerializeField] private int[] _generateWeights;
    [SerializeField] private float _initInterval = 0.5f;
    [SerializeField] private float _minLimmitInterval = 0.1f;
    [SerializeField] private float _lifeTime = 5f;

    private SushiTouch[] _generatePrefabs;
    private SushiMenu _sushiMenu;
    private float _currentInterval = 0.5f;
    private float _timer = 0;

    // 種類ごとのプールを Dictionary で管理
    private Dictionary<SushiType, ObjectPool<SushiTouch>> _sushiPools;

    public Dictionary<SushiType, ObjectPool<SushiTouch>> SushitPools => _sushiPools;

    void Start()
    {
        _sushiMenu = FindAnyObjectByType<SushiMenu>();
        _generatePrefabs = _sushiMenu.AllSushuis;
        _sushiPools = new Dictionary<SushiType, ObjectPool<SushiTouch>>();


        foreach (var prefab in _generatePrefabs)
        {
            SushiType type = prefab.SushiParameterData.sushiParameter.Type; // ← SushiMove 内で SushiParameter を持っている想定
            //Debug.Log($"[Pool Init] Register {type} from prefab {prefab.name}");

#if UNITY_EDITOR
            // プレハブ名と SushiType の不一致チェック
            if (!prefab.name.Contains(type.ToString()))
            {
                Debug.LogWarning($"[Pool Init] Prefab {prefab.name} の Type が {type} になっています。設定ミスの可能性があります！");
            }
#endif

            //if (_sushiPools.ContainsKey(type))
            //{
            //    Debug.LogWarning($"[Pool Init] {type} はすでに登録されています。上書きします。");
            //}

            var pool = new ObjectPool<SushiTouch>(
                createFunc: () =>
                {
                    SushiTouch obj = Instantiate(prefab);
                    obj.gameObject.SetActive(false);
                    return obj;
                },
                onGet: (obj) => obj.gameObject.SetActive(true),
                onRelease: (obj) =>
                {
                    obj.SushiMove?.SetDirection(MoveDirectionType.Stop);
                    obj.gameObject.SetActive(false);
                },
                initialSize: 5
            );

            // 先に Dictionary に登録してから明示的に初期化
            _sushiPools[type] = pool;

            foreach (SushiTouch obj in _sushiPools[type].Pool)
            {
                obj.SetPool(pool);
            }
        }

    }

    void Update()
    {
        if (_timer > _currentInterval)
        {
            if (_leftGenerateTransforms.Length <= 0 || _generatePrefabs.Length <= 0 || _generateWeights.Length <= 0) return;

            Vector3 generatePosition = Vector3.zero;
            //int randamLR = Random.Range(0, 2); // 0: left, 1: right

            //if (randamLR == 0)
            //{
            //    generatePosition = _leftGenerateTransforms[Random.Range(0, _leftGenerateTransforms.Length)].position;
            //}
            //else
            //{
            generatePosition = _rightGenerateTransforms[Random.Range(0, _rightGenerateTransforms.Length)].position;
            //Debug.Log($"Generate Podition : {generatePosition}");
            //}

            int num = Choose(_generateWeights);
            SushiType type = _generatePrefabs[num].SushiParameterData.sushiParameter.Type;

            if (!_sushiPools.ContainsKey(type))
            {
#if UNITY_EDITOR
                Debug.LogError($"[SushiGenerator] {type} が _sushiPools に存在しません！ " +
                               $"num={num}, prefab={_generatePrefabs[num].name}");
#endif
                return;
            }

            if (_sushiPools.ContainsKey(type))
            {
                SushiTouch sushiTouch = _sushiPools[type].Get(_lifeTime);

                // SushiMenu から FreeSushiState を取得して設定
                if (_sushiMenu != null)
                {
                    sushiTouch.SetFreeSushiState(_sushiMenu.GetFreeSushiState());
                }

                SushiMove sushiMove = sushiTouch.SushiMove;
                sushiMove.gameObject.transform.position = generatePosition;
                sushiMove.SetDirection(MoveDirectionType.Left);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Choose not registration type : {type} !!\nMust registration that type object!!");
#endif
            }

            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    private void OnDestroy()
    {
        // シーン遷移時にプールをクリーンアップ
        if (_sushiPools is not null)
        {
            foreach (var pool in _sushiPools.Values)
            {
                pool.Cleanup();
            }
        }
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
